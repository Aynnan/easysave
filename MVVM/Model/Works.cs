using System.Collections.Generic;

namespace EasySave.MVVM.Model
{
    class Works
    {
        WorksJson worksJson;
        WorksXml worksXml;

        public Works()
        {
            worksJson = new WorksJson();
            worksXml = new WorksXml();
        }
        public void Add(Work work)
        {
            List<Work> list = getList();
            list.Add(work);
            worksJson.set(list);
            worksXml.set(list);
        }
        public List<Work> getList()
        {
            List<Work> works;
            works = worksJson.get();
            return works;
        }
        public void Modify(Work work, int index) { 
            List<Work> works = getList();
            works[index] = work;
            worksJson.set(works);
            worksXml.set(works);
        }
    }
}
