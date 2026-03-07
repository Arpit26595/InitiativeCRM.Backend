namespace Shared.Models.Utilities.DataTableModel
{

    public class DataTableAjaxPostModel<T>
    {
        // properties are not capital due to json mapping
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public List<Column> columns { get; set; }
        public Search search { get; set; }
        public List<SortOrder> order { get; set; }
        public T SearchModel { get; set; }
    }

    public class DataTableAjaxResultsModel<TResult>
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public TResult data { get; set; }
    }
    public class Column
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public Search search { get; set; }
    }

    public class Search
    {
        public string value { get; set; }
        public string regex { get; set; }
    }

    public class SortOrder
    {
        public int column { get; set; }
        public string dir { get; set; }
    }
}
