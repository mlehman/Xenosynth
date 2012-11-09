using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;

namespace Xenosynth.Admin.Controls.WebParts {

    public class RegisteredCatalogZone : CatalogPart {


        public override string Title {
            get {
                string title = base.Title;
                return string.IsNullOrEmpty(title) ? "Registered Web Parts" : title;
            }
            set {
                base.Title = value;
            }
        }


        public RegisteredCatalogZone() {

        }


        public override WebPartDescriptionCollection GetAvailableWebPartDescriptions() {

            if (this.DesignMode) {
                return new WebPartDescriptionCollection(new object[] {
                    new WebPartDescription("1", "Registered WebPart 1", null, null),
                    new WebPartDescription("2", "Registered WebPart 2", null, null),
                        new WebPartDescription("3", "Registered WebPart 3", null, null)});
            }

            IList registeredWebParts = RegisteredWebPart.FindAll();

            IList list = new ArrayList();
            foreach(RegisteredWebPart registeredWebPart in registeredWebParts) {
                list.Add(
                    new WebPartDescription(registeredWebPart.ID.ToString(), registeredWebPart.Title, registeredWebPart.Description, registeredWebPart.ImageUrl)
                );
            }
            return new WebPartDescriptionCollection(list);
        }

        /// <summary>
        /// Returns a new instance of the WebPart specified by the description
        /// </summary>
        public override WebPart GetWebPart(WebPartDescription description) {

            Guid id = new Guid(description.ID);
            RegisteredWebPart rwp = RegisteredWebPart.FindByID(id);

            Control control = this.Page.LoadControl(rwp.Url);
            control.ID = description.ID;
            return this.WebPartManager.CreateWebPart(control);

            //string typeName = this.GetTypeNameFromXml(description.ID);
            //Type type = Type.GetType(typeName);
            //return Activator.CreateInstance(type, null) as WebPart;
        }

    }
}
