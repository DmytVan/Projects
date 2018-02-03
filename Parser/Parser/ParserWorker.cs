using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Parser
{
    class ParserWorker
    {
        public static int count = 0;
        bool catigories = false;
        //List<Product> product = new List<Product>();
        HtmlLoader loader = new HtmlLoader();
        IParse parser = new IParse() ;
        string prefix = "/search/?text={0}&p={1}";

        public void Start(string name, ComboBox cm, Label lb1)
        {
            catigories = false;
            parser.pageNumb = 0;
            Worker(name, cm, lb1);
            parser.ListClear();
        }

        public void Start(int i, ComboBox cm, Label lb1)
        {
            try
            {
                catigories = true;
                parser.pageNumb = 0;
                Worker(parser.catigoriesHref.ElementAt(i), cm, lb1);
                parser.ListClear();
            }catch(Exception){}
    }

        public async void Worker(string name, ComboBox cm, Label lb1)
        {
 
            string sourse = "";
            try
            {
                if (!catigories)
                {
                    sourse = await loader.GetSourse(string.Format(prefix, name, 0));
                }
                else
                {
                    sourse = await loader.GetSourse(name);
                }
            }catch(Exception)
            {
                return;
            }
            var domParser = new HtmlParser();
            var document = await domParser.ParseAsync(sourse);
            parser.pageNumb = 0;
            parser.ListClear();
            parser.SetCount();
            parser.Parse(document);
            lb1.Content = IParse.result;
            parser.ParseCatigories(document);
            cm.Items.Refresh();

            //   Console.WriteLine(parser.pageNumb);

            if (parser.pageNumb > 1)
                for (int i = 1; i < parser.pageNumb; i++)
                {
                    if (count != IParse.count)
                        return;
                        sourse = await loader.GetSourse(string.Format(prefix, name, i));
                        domParser = new HtmlParser();
                        document = await domParser.ParseAsync(sourse);
                        parser.Parse(document);
                    cm.Items.Refresh();
                }
            cm.Items.Refresh();
          //  Console.ReadKey();
        }

    }
}
