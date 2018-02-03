using AngleSharp.Dom.Html;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Parser
{
    class IParse
    {
        public static int count;
        public int pageNumb = 0;
        public static ObservableCollection<Product> product = new ObservableCollection<Product>();
        public List<string> catigoriesHref = new List<string>();
        public static List<string> catigoriesName = new List<string>();
        public static string result = "";


        public void Parse(IHtmlDocument document)
        {
            if (count != ParserWorker.count)
                return;
            var nameProducts = document.QuerySelectorAll("a").Where(item => item.ClassName != null && item.ClassName.Contains("g-i-title-link novisited"));
            var productsPrice = document.QuerySelectorAll("").Where(item => item.ClassName != null && item.ClassName.Contains("g-i-price"));
            var itemImg = document.QuerySelectorAll("img[src]").Where(item => item.ClassName == null && item.GetAttribute("src").Contains("http://i.tavriav.ua/goods"));
            var page = document.QuerySelectorAll("a").Where(item => item.ClassName != null && item.ClassName.Contains("b-paginator-l-i-link txt-black"));
            var resultParse = document.QuerySelectorAll("").Where(item => item.ClassName != null && item.ClassName.Contains("search-filter-counter"));
            // var items = document.QuerySelectorAll("a").Where(item => item.ClassName != null && item.ClassName.Contains("post__title_link"));  list.Add(item.TextContent);
            // var items = document.QuerySelectorAll("img[src]").Where(item => item.ClassName == null );

            if (pageNumb == 0 && page.ToArray().Length > 0)
                Int32.TryParse(page.ElementAt(page.ToArray().Length - 1).TextContent, out pageNumb);

            if (resultParse.ToArray().Length > 0)
                result = resultParse.ElementAt(resultParse.ToArray().Length - 1).TextContent;
            else
                result = "Ничего не найдено";

            if (count != ParserWorker.count)
                return;
            for (int i = 3; i < productsPrice.ToArray().Length; i++)
            {
                var imgUrl = itemImg.ElementAt(i).GetAttribute("src");
                var name = nameProducts.ElementAt(i);
                var price = productsPrice.ElementAt(i);
                product.Add(new Product(name.TextContent, price.TextContent, imgUrl, "http://www.tavriav.ua/maslo-selyanske-73-200-g/p10322/"));
            }
           
        }
        public void ParseCatigories(IHtmlDocument document)
        {
            var hrefCatigories = document.QuerySelectorAll("a").Where(item => item.ClassName != null && item.ClassName.Contains("search-filter-c-l-i-link"));
            var nameCatigories = document.QuerySelectorAll("a").Where(item => item.ClassName != null && item.ClassName.Contains("search-filter-c-l-i-link"));

            foreach (var item in hrefCatigories)
            {
                catigoriesHref.Add(item.GetAttribute("href"));
            }

            foreach (var item in nameCatigories)
            {
                catigoriesName.Add(item.TextContent);
            }
        }

        public void ListClear()
        {
            product.Clear();
            catigoriesHref.Clear();
            catigoriesName.Clear();
        }

        public void SetCount()
        {
            count = ParserWorker.count;
        }
    }
}
