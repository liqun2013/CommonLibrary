function ChunkedUploader(file, options) {
	if (!this instanceof ChunkedUploader) {
		return new ChunkedUploader(file, options);
	}

	if (file) {
		this.file = file;
		this.file_size = this.file.size;

		if ('mozSlice' in this.file) {
			this.slice_method = 'mozSlice';
		}
		else if ('webkitSlice' in this.file) {
			this.slice_method = 'webkitSlice';
		}
		else {
			this.slice_method = 'slice';
		}
	}

	this.maximumFileSize = 2097152*200; //2M
	this.options = options;
	this.chunk_size = (1024 * 128); // 64KB
	this.range_start = 0;
	this.range_end = this.chunk_size;
	this.fileUrl = '';
	this.fileFullPath = '';
	this.containerId = '';
	this.illegalFileTypes = ["exe", "dll", "js", "app", "class", "c", "h", "bac", "com", "apk", "bundle", "bpl", "coff", "dcu", "jar", "o", "dylib", "so", "war", "xbe", "xap", "xcoff", "wxw", "vbx", "oxc", "tlb", "sql", "htm", "html", "css", "dat", "db", "jsfl", "php", "xhtml", "cs", "asp", "aspx", "mhtml", "cgi", "jsp", "rna", "json", "eml", "swa", "cpp"];

	var self = this;

	this.upload_request = new XMLHttpRequest();
	this.upload_request.onload = function () { self._onChunkComplete(); };

	if ('onLine' in navigator) {
		window.addEventListener('online', this._onConnectionFound);
		window.addEventListener('offline', this._onConnectionLost);
	}
}

ChunkedUploader.prototype = {
	_upload: function () {
		var self = this, chunk;

		setTimeout(function () {
			if (self.range_end > self.file_size) {
				self.range_end = self.file_size;
			}
			chunk = self.file[self.slice_method](self.range_start, self.range_end);

			self.upload_request.open('POST', self.options.url, true);
			self.upload_request.overrideMimeType('application/octet-stream');

			self.upload_request.setRequestHeader('Content-FileName', encodeURI(self.file.name));
			self.upload_request.setRequestHeader('Content-FileType', self.file.type);
			self.upload_request.setRequestHeader('Content-FileSize', self.file_size);

			self.upload_request.setRequestHeader('Content-RangeStart', self.range_start);
			self.upload_request.setRequestHeader('Content-RangeEnd', self.range_end);

			self.upload_request.send(chunk);
		}, 20);
	},
	_showError: function () {
		var self = this;
		$(self.options.errMsgContainer).text('error occured!').removeClass('hidden');
	},

	_onChunkComplete: function () {
		var $selectedfile = $('#' + this.containerId);
		var $btn = $selectedfile.find('button.uploadfile-doupload');
		$btn.html(Math.ceil((this.range_end / this.file_size) * 100) + "%");

		var resp = JSON.parse(this.upload_request.response);

		if (resp && resp.NeedLogin) { this.reLogin(); }
		if (this.range_end === this.file_size) {
			if (resp.FileUrl) { this.fileUrl = resp.FileUrl }
			this._onUploadComplete();
			return;
		}

		if (resp.Error) {
			this._showError();
			return;
		}

		if (resp.Success) {
			if (resp.UploadedRange) {
				this.range_start = resp.UploadedRange;
				this.range_end = this.range_start + this.chunk_size;
			}
			else if (resp.UploadedRange == 0) {
				this.range_start = resp.UploadedRange;
				this.range_end = this.range_start + this.chunk_size;
			}
			else {
				this.range_start = this.range_end;
				this.range_end = this.range_start + this.chunk_size;
			}

			if (!this.is_paused) {
				this._upload();
			}
		}
	},

	_onUploadComplete: function () {
		var self = this;
		if (self.options.uploadCompleteCallback && typeof (self.options.uploadCompleteCallback) === "function") {
			self.options.uploadCompleteCallback(self.fileUrl);
		}
		self.fileFullPath = self.fileUrl;
	},

	_onConnectionFound: function () {
		this.resume();
	},

	_onConnectionLost: function () {
		this.pause();
	},

	start: function () {
		$('#' + this.containerId).find("button.uploadfile-doupload").attr('disabled', "disabled");
		this._upload();
	},

	pause: function () {
		this.is_paused = true;
	},

	resume: function () {
		this.is_paused = false;
		this._upload();
	},

	isFileTypeIllegal: function (ft) {
		var self = this;
		if (ft) {
			for (var i = 0; i < self.illegalFileTypes.length; i++) {
				if (self.illegalFileTypes[i] == ft.toLowerCase())
					return true;
			}
		}
		return false;
	},

	setFile: function (f, cid) {
		if (f) {
			this.file = f;
			this.file_size = this.file.size;
			if ('mozSlice' in this.file) {
				this.slice_method = 'mozSlice';
			}
			else if ('webkitSlice' in this.file) {
				this.slice_method = 'webkitSlice';
			}
			else {
				this.slice_method = 'slice';
			}
		}
		//this.chunk_size = (1024 * 64); // 64KB
		this.range_start = 0;
		this.range_end = this.chunk_size;
		this.containerId = cid;
	},
	reLogin: function () {
		var $loginModal = $('#asyncLoginModal');
		$loginModal.modal('show');
	}
};

function MultiFileUploader(opts) {
	if (!this instanceof MultiFileUploader) {
		return new MultiFileUploader(options);
	}

	this.chunked_uploaders = [];
	this.options = opts;
}

MultiFileUploader.prototype = {
	addFileUploader: function (fu) {
		this.chunked_uploaders.push(fu);
	},
	deleteFile: function (cid) {
		$('#' + cid).remove();
		for (var i = 0; i < this.chunked_uploaders.length; i++) {
			var itm = this.chunked_uploaders[i];
			if (itm.containerId == cid) {
				this.chunked_uploaders.splice(i, 1);
				break;
			}
		}
	},
	uploadFile: function (cid) {
		for (var i = 0; i < this.chunked_uploaders.length; i++) {
			var itm = this.chunked_uploaders[i];
			if (itm.containerId == cid) {
				itm.start();
				break;
			}
		}
	},
	isUploadComplete: function () {
		if (this.chunked_uploaders.length > 0) {
			for (var i = 0; i < this.chunked_uploaders.length; i++) {
				var itm = this.chunked_uploaders[i];
				var btnUpload = $('#' + itm.containerId).find('button.uploadfile-doupload');
				if (btnUpload.length > 0 && btnUpload.text() != '100%') {
					return false;
				}
			}
		}

		return true;
	}
}