using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCorePaginator
{
    public class PagedList<T>
    {

        public List<LinkInfo> Links { get; set; }
        public int Total { get; set; }
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
        public List<T> Data { get; set; }
        public int TotalPages => (int)Math.Ceiling(this.Total / (double)this.PerPage);
        public bool HasPreviousPage => this.CurrentPage > 1;
        public bool HasNextPage => this.CurrentPage < this.TotalPages;
        public int NextPageNumber => this.HasNextPage ? this.CurrentPage + 1 : this.TotalPages;
        public int PreviousPageNumber => this.HasPreviousPage ? this.CurrentPage - 1 : 1;

        public int From => GetFrom();
        public int To => GetTo();

        public string NextPageUrl { get; internal set; }
        public string PrevPageUrl { get; internal set; }

        public PagedList()
        {

        }
        public PagedList(IQueryable<T> source, PagingParams pagingParams)
        {
           
            this.CurrentPage = pagingParams != null? pagingParams.PageNumber: 1;
            this.PerPage = pagingParams != null ? pagingParams.PageSize: 10;
            this.Total = source.Count();
            this.Data = GenearateList(source);
        }

        public PagedList(IQueryable<T> source, int currentPage, int pageSize)
        {
           
            this.CurrentPage = currentPage;
            this.PerPage = pageSize;
            this.Total = source.Count();
            this.Data = GenearateList(source);
        }

        private List<T> GenearateList(IQueryable<T> source)
        {
            return source.Skip(PerPage * (CurrentPage - 1))
                            .Take(PerPage)
                            .ToList();
        }

        private int GetFrom(){
            if(CurrentPage == 1) return 1;
            else return ((CurrentPage -1)  * PerPage) + 1;
        }

        private int GetTo(){
            var next = CurrentPage  * PerPage;
            if (next <= Total){
              return next;
            }

            var from = GetFrom();
            return from + (Total - from);
        }

        public PagingHeader GetHeader()
        {
            return new PagingHeader(
                 this.Total, 
                 this.CurrentPage,
                 this.PerPage, 
                 this.TotalPages
           );
        }
    }
}
  
