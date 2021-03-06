Number.prototype.formatBytes = function () {
	var units = ['B', 'KB', 'MB', 'GB', 'TB'], bytes = this, i;
	for (i = 0; bytes >= 1024 && i < 4; i++) {
		bytes /= 1024;
	}

	return bytes.toFixed(2) + units[i];
}
Date.prototype.formatDatetime = function (fmt) {
	var o = {
		"M+": this.getMonth() + 1,
		"d+": this.getDate(),
		"h+": this.getHours(),
		"m+": this.getMinutes(),
		"s+": this.getSeconds(),
		"q+": Math.floor((this.getMonth() + 3) / 3),
		"S": this.getMilliseconds()
	};
	if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
	for (var k in o)
		if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
	return fmt;
}
function Utilities(options) {
	if (!this instanceof Utilities) { return new Utilities(options) }
	this.options = options;
	this.timeout = 0;
}
Utilities.prototype = {
	_hideMessageBox: function () {
		var self = this;
		var $messageBoxContainer = $(self.options.msgBoxId);
		if ($messageBoxContainer) {
			$messageBoxContainer.addClass('hidden')
		}
		if (self.timeout > 0) {
			clearTimeout(self.timeout)
		}
		if (self.options.redirect && self.options.redirectUrl) {
			window.top.location.replace(self.options.redirectUrl);
		}
	},
	alertSuccessMsg: function (msg) {
		var self = this;
		var $messageBoxContainer = $(self.options.msgBoxId);

		$messageBoxContainer.removeClass('alert-info');
		$messageBoxContainer.removeClass('alert-danger');
		$messageBoxContainer.addClass('alert-success');
		$messageBoxContainer.text(msg);
		$messageBoxContainer.removeClass('hidden');
		if (self.options.secconds > 0) {
			self.timeout = setTimeout(self._hideMessageBox.bind(this), self.options.secconds * 1000);
		}
	},
	alertInfoMsg: function (msg) {
		var self = this;
		var $messageBoxContainer = $(self.options.msgBoxId);
		$messageBoxContainer.removeClass('alert-success');
		$messageBoxContainer.removeClass('alert-danger');
		$messageBoxContainer.addClass('alert-info');
		$messageBoxContainer.text(msg);
		$messageBoxContainer.removeClass('hidden');
		if (self.options.secconds > 0) {
			self.timeout = setTimeout(self._hideMessageBox.bind(this), self.options.secconds * 1000);
		}
	},
	alertErrorMsg: function (msg) {
		var self = this;
		var $messageBoxContainer = $(self.options.msgBoxId);
		$messageBoxContainer.removeClass('alert-success');
		$messageBoxContainer.removeClass('alert-info');
		$messageBoxContainer.addClass('alert-danger');
		$messageBoxContainer.text(msg);
		$messageBoxContainer.removeClass('hidden');
		if (self.options.secconds > 0) {
			self.timeout = setTimeout(self._hideMessageBox.bind(this), self.options.secconds * 1000);
		}
	},
	buildPaginateNav: function (paginateList) {
		var self = this;
		var h = [];

		if (paginateList && paginateList.TotalPageCount > 0) {
			var start;
			var end;
			for (var i = 0; i <= 3; i++) {
				start = paginateList.PageIndex - i;
				if (start <= 1) { break }
			}
			for (var i = 0; i <= 3; i++) {
				end = paginateList.PageIndex + i;
				if (end >= paginateList.TotalPageCount) { break }
			}
			h.push('<fieldset class="pagination-container">');
			h.push('  <nav>');
			h.push('	  <ul class="pagination pagination-sm">');
			if (start > 1) {
				if (1 == paginateList.PageIndex) {
					h.push('	  <li class="paging active"><span>1 <span class="sr-only">(current)</span></span></li>')
				}
				else {
					h.push('	  <li class="paging"><a href="javascript:;">1 </a></li>')
				}
			}
			if (start > 2) {
				h.push('	  <li class=""><a href="javascript:;">...</a></li>');
			}
			for (var i = start; i <= end; i++) {
				if (i == paginateList.PageIndex) {
					h.push('	  <li class="paging active"><span>' + i + ' <span class="sr-only">(current)</span></span></li>');
				}
				else {
					h.push('	  <li class="paging"><a href="javascript:;">' + i + '</a></li>');
				}
			}
			if (end + 1 < paginateList.TotalPageCount) {
				h.push('	  <li class=""><a href="javascript:;">...</a></li>');
			}
			if (end < paginateList.TotalPageCount) {
				if (paginateList.TotalPageCount == paginateList.PageIndex) {
					h.push('	  <li class="paging active"><span>' + paginateList.TotalPageCount + ' <span class="sr-only">(current)</span></span></li>');
				}
				else {
					h.push('	  <li class="paging"><a href="javascript:;">' + paginateList.TotalPageCount + '</a></li>');
				}
			}
			if (paginateList.TotalPageCount > 9) {
				h.push('    <li><select class="form-control pageindexto" style="display:inline;height:30px;WIDTH: 80px; FLOAT: left;">');
				for (var i = 1; i <= paginateList.TotalPageCount; i++)
					if(paginateList.PageIndex == i)
						h.push('<option selected="selected">' + i + '</option>');
					else
						h.push('<option>' + i + '</option>');
				h.push('</select></li>');
			}
			h.push('	  </ul>');
			h.push('  </nav>');
			h.push('</fieldset>');
		}

		$container = $(self.options.paginateContainer);
		$container.html(h.join(''));

		$container.undelegate(".paging", "click");
		$container.delegate(".paging", "click", function () {
			if ($(this).find('a').hasClass("disabled"))
				return;

			var index = parseInt($(this).find('a').text());
			var fs = $(this).parents('fieldset');
			fs.attr("disabled","disabled");
			fs.find('a').addClass("disabled");

			if (index && index != self.options.pageIndex) {
				self.options.pageIndex = index;
				self.loadData();
			}
			else
				self.loadData();

			return false;
		});
		$container.undelegate(".pageindexto", "change");
		$container.delegate(".pageindexto", "change", function (e) {
			if ($(this).find('a').hasClass("disabled"))
				return;

			var index = parseInt($(this).val());
			var fs = $(this).parents('fieldset');
			fs.attr("disabled","disabled");
			fs.find('a').addClass("disabled");

			if (index && index != self.options.pageIndex) {
				self.options.pageIndex = index;
				$(this).val(index);
				self.loadData();
			}
			else
				self.loadData();
		});
	},
	loadData: function () {
		var self = this;
		if (self.options.loadPaginateData && typeof (self.options.loadPaginateData) === "function") {
			self.options.loadPaginateData(self.options);
		}
	},
	alertConfirmModal: function () {
		var self = this;
		var modalHtml = '<div class="modal " tabindex="-1" role="dialog" id="confirmModal" name="confirmModal">' +
			'<div class="modal-dialog" role="document">' +
			'<div class="modal-content">' +
			'<div class="modal-header">' +
			'<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
			'<h4 class="modal-title">' + self.options.modalTitle + '</h4>' +
			'</div>' +
			'<div class="modal-body">' +
			'<p>' + self.options.modalContent + '</p>' +
			'</div>' +
			'<div class="modal-footer">' +
			'<button type="button" class="btn btn-default" data-dismiss="modal">' + self.options.noButtonText + '</button>' +
			'<button type="button" class="btn btn-primary yesbutton">' + self.options.yesButtonText + '</button>' +
			'</div>' +
			'</div>' +
			'</div>' +
			'</div>';

		if ($('#confirmModal').length == 0) {
			$('body').append(modalHtml);
		}
		var $confirmModal = $('#confirmModal');
		$confirmModal.modal('show');
		$confirmModal.undelegate(".yesbutton", "click");
		$confirmModal.delegate(".yesbutton", "click", function () {
			if (self.options.confirmYesCallBack && typeof (self.options.confirmYesCallBack) === "function") {
				self.options.confirmYesCallBack();
				$confirmModal.modal('hide');
			}
		});
	},
	htmlEncode: function (html) {
		return document.createElement('a').appendChild(document.createTextNode(html)).parentNode.innerHTML;
	},
	reLogin: function () {
		var $loginModal = $('#asyncLoginModal');
		$loginModal.modal('show');
	},
	generateRandomString: function (l) {
		var x = "qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM";
		var tmp = "";
		var timestamp = new Date().getTime();
		for (var i = 0; i < l; i++) {
			tmp += x.charAt(Math.ceil(Math.random() * 100000000) % x.length);
		}
		return tmp + timestamp;
	},
	reloadPage: function (sec) {
		setTimeout('location.reload(true)', sec * 1000);
	},
	editIcon: function () {
		return '<svg class="bi bi-pencil" width="1em" height="1em" viewBox="0 0 16 16" fill="currentColor" xmlns="http://www.w3.org/2000/svg"><path fill-rule="evenodd" d="M11.293 1.293a1 1 0 0 1 1.414 0l2 2a1 1 0 0 1 0 1.414l-9 9a1 1 0 0 1-.39.242l-3 1a1 1 0 0 1-1.266-1.265l1-3a1 1 0 0 1 .242-.391l9-9zM12 2l2 2-9 9-3 1 1-3 9-9z" /><path fill-rule="evenodd" d="M12.146 6.354l-2.5-2.5.708-.708 2.5 2.5-.707.708zM3 10v.5a.5.5 0 0 0 .5.5H4v.5a.5.5 0 0 0 .5.5H5v.5a.5.5 0 0 0 .5.5H6v-1.5a.5.5 0 0 0-.5-.5H5v-.5a.5.5 0 0 0-.5-.5H3z" /></svg>';
	},
	deleteIcon: function () {
		return '<svg class="bi bi-trash" width="1em" height="1em" viewBox="0 0 16 16" fill="currentColor" xmlns="http://www.w3.org/2000/svg"><path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6z" /><path fill-rule="evenodd" d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1v1zM4.118 4L4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4H4.118zM2.5 3V2h11v1h-11z" /></svg>';
	}
}
