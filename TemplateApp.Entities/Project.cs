using System;


namespace TemplateApp.Entities
{
    public class Project
    {
        #region properties

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        #endregion
    }
}
