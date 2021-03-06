using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using PX.Common;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CR.MassProcess;
using PX.Objects.GL;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.SM;
using PX.Objects;
using PX.Objects.CR;


namespace PX.Objects.CR
{
  public class ContactMaint_Extension : PXGraphExtension<ContactMaint>
  {
        public PXSetup<CRSetup> setup;
  
        public PXAction<Contact> callPhone1;
        [PXUIField(DisplayName = "Call", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
        [PXButton]
        public virtual IEnumerable CallPhone1(PXAdapter adapter)
        {
            Contact contact = Base.Contact.Current; 
            MyPut(contact.Phone1);

            return adapter.Get();
        }
          
        public PXAction<Contact> callPhone2;
        [PXUIField(DisplayName = "Call", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
        [PXButton]
        public virtual IEnumerable CallPhone2(PXAdapter adapter)
        {
            Contact contact = Base.Contact.Current; 
            MyPut(contact.Phone2);
  
            return adapter.Get();
        }
          
        public PXAction<Contact> callPhone3;
        [PXUIField(DisplayName = "Call", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
        [PXButton]
        public virtual IEnumerable CallPhone3(PXAdapter adapter)
        {
            Contact contact = Base.Contact.Current; 
            MyPut(contact.Phone3);

            return adapter.Get();
        }

        public void MyPut(string phoneNumber)
        {

            CRSetupExt ext = setup.Current.GetExtension<CRSetupExt>();
            string url = ext.UsrVOIPUrl;

            EPEmployee employee = PXSelect<EPEmployee,
                Where<EPEmployee.userID, Equal<Required<EPEmployee.userID>>>>.Select(Base, Base.Accessinfo.UserID);

            BAccountExt baccountExtension = employee.GetExtension<BAccountExt>();
            string phoneExt = baccountExtension.UsrPhoneExtension;

            HttpClient client = new HttpClient(
                new HttpClientHandler
                {
                    UseCookies = true,
                    CookieContainer = new CookieContainer()
                })
            {
                BaseAddress = new Uri("http://" + url + "/"),
                DefaultRequestHeaders =
                    {
                        Accept = { MediaTypeWithQualityHeaderValue.Parse("text/json") }
                    }
            };

            var response = client.PostAsync("http://" + url + "/call.php?exten=" + phoneExt + "&number=" + phoneNumber, 
                new StringContent("", System.Text.Encoding.UTF8, "application/json")).Result;
            var content = response.Content.ReadAsStringAsync().Result;
            response.EnsureSuccessStatusCode();
 

            CRActivityMaint graph = PXGraph.CreateInstance<CRActivityMaint>();
            CRActivity activity = new CRActivity();
            activity = graph.Activities.Insert(activity);
            activity.Type = "P";
            activity.Subject = "Outbound Call";
            activity.OwnerID = employee.UserID;
            activity.RefNoteID = Base.ContactCurrent.Current.NoteID;
            graph.Activities.Update(activity);

            PXRedirectHelper.TryRedirect(graph, PXRedirectHelper.WindowMode.NewWindow);


        }       

    #region Event Handlers

    #endregion
  }
}