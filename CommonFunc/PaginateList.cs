using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonLibrary
{
	public class PaginateList
	{
		public List<BaseDto> Items { get; private set; }
		public int PageIndex { get; private set; }
		public int PageSize { get; private set; }
		public int TotalCount { get; private set; }
		public int TotalPageCount { get; private set; }
		public PaginateList()
		{
			PageIndex = 1;
			PageSize = 10;
			TotalCount = 0;
			TotalPageCount = 0;
		}
		public PaginateList(int pageIndex, int pageSize)
		{
			PageIndex = pageIndex;
			PageSize = pageSize;
			TotalCount = 0;
			TotalPageCount = 0;
		}
		/// <summary>
		/// 数据已分好页的构造
		/// </summary>
		/// <param name="pageIndex"></param>
		/// <param name="pageSize"></param>
		/// <param name="totalCount"></param>
		/// <param name="source"></param>
		public PaginateList(int pageIndex, int pageSize, int totalCount, IEnumerable<BaseDto> source)
		{
			SetPaginatedData(source, pageIndex, totalCount, pageSize);
		}
		/// <summary>
		/// 数据未分好页的构造
		/// </summary>
		/// <param name="pageIndex"></param>
		/// <param name="pageSize"></param>
		/// <param name="totalCount"></param>
		/// <param name="source"></param>
		public PaginateList(int pageIndex, int pageSize, IEnumerable<BaseDto> source)
		{
			SetNotPaginatedData(source, pageIndex, pageSize);
		}
		/// <summary>
		/// 设置未分好页的数据
		/// </summary>
		/// <param name="data">未分好页的数据</param>
		/// <param name="pageIndex">第几页</param>
		/// <param name="pageSize">每页几条</param>
		public void SetNotPaginatedData(IEnumerable<BaseDto> data, int pageIndex, int pageSize = 10)
		{
			if (data != null && data.Any())
			{
				PageIndex = pageIndex;
				PageSize = pageSize;
				TotalCount = data.Count();
				TotalPageCount = (int)Math.Ceiling(TotalCount / (double)pageSize);
				Items = data.Skip((pageIndex - 1) * PageSize).Take(PageSize).ToList();
			}
			else
			{
				PageIndex = 1;
				PageSize = pageSize;
				TotalCount = 0;
				TotalPageCount = 0;
			}
		}
		/// <summary>
		/// 设置分好页的数据
		/// </summary>
		/// <param name="data">分好页的数据</param>
		/// <param name="pageIndex">第几页</param>
		/// <param name="pageSize">每页几条</param>
		public void SetPaginatedData(IEnumerable<BaseDto> data, int pageIndex, int totalCount, int pageSize = 10)
		{
			if (data != null && data.Any())
			{
				PageIndex = pageIndex;
				PageSize = pageSize;
				TotalCount = totalCount;
				TotalPageCount = (int)Math.Ceiling(TotalCount / (double)pageSize);
				Items = data.ToList();
			}
			else
			{
				PageIndex = 1;
				PageSize = pageSize;
				TotalCount = 0;
				TotalPageCount = 0;
			}
		}
		public bool HasPreviousPage => (PageIndex > 1);
		public bool HasNextPage => (PageIndex < TotalPageCount);
	}
}
