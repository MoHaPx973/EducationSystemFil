namespace EducationSystem.BlazorServer.Pages.SharedHelper
{
    public class Filter
    {
        public Filter(Filter newFilter)
        {
            SelectValue1 = newFilter.SelectValue1;
            SelectValue2 = newFilter.SelectValue2;
            SelectValue3 = newFilter.SelectValue3;
            SelectValue4 = newFilter.SelectValue4;
            SelectValue5 = newFilter.SelectValue5;
        }

        public Filter()
        {
            SelectValue1 = 0;
            SelectValue2 = 0;
            SelectValue3 = 0;
            SelectValue4 = 0;
            SelectValue5 = 0;
        }

        public int SelectValue1 { get; set; } = 0;
        public int SelectValue2 { get; set; } = 0;
        public int SelectValue3 { get; set; } = 0;
        public int SelectValue4 { get; set; } = 0;
        public int SelectValue5 { get; set; } = 0;

    }
}
