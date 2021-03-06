using System;
using PX.Data;

namespace FreePBXIntegration
{
  [Serializable]
  public class PhoneCallerAudit : IBqlTable
  {
    #region RecordID
    [PXDBLongIdentity(IsKey = true)]
    public virtual long? RecordID { get; set; }
    public abstract class recordID : IBqlField { }
    #endregion

    #region PhoneNubmer
    [PXDBString(32, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Phone Nubmer")]
    public virtual string PhoneNubmer { get; set; }
    public abstract class phoneNubmer : IBqlField { }
    #endregion

    #region CallerID
    [PXDBString(32, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Caller ID")]
    public virtual string CallerID { get; set; }
    public abstract class callerID : IBqlField { }
    #endregion
      
    #region ContactID
    [PXDBInt()]
    [PXUIField(DisplayName = "Contact ID")]
    public virtual int? ContactID{ get; set; }
    public abstract class contactID: IBqlField { }
    #endregion 

    #region Tstamp
    [PXDBTimestamp()]
    [PXUIField(DisplayName = "Tstamp")]
    public virtual byte[] Tstamp { get; set; }
    public abstract class tstamp : IBqlField { }
    #endregion

    #region Noteid
    [PXDBGuid()]
    [PXUIField(DisplayName = "Noteid")]
    public virtual Guid? Noteid { get; set; }
    public abstract class noteid : IBqlField { }
    #endregion
  }
}