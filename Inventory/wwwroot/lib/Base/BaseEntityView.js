/*
  _Params = 
  {
    FormID: null,
    SubmitButtonID: null,   //This can be a class or a id
    ShowSavingDialog: true,
    ValidationFailedCallBack: 
    RemoveEntityCallBack
  }
*/

function BaseEntityView(params) {
  var _Params = params;
  var _ObjectReference = this;

  _Params.Type = "BaseEntityView";
  _Params.InstanceCount = ClassLoader.InstanceCounter();
  console.log("INIT: " + _Params.Type + " - " + _Params.InstanceCount);

  this.SubmitForm = function (skipValidation)
  {
    
    if (_Params.ShowSavingDialog)
    {
      _Params.SavingDialog = SurgeOneSpinner("Saving");
    }

    if (skipValidation == true)
    {
      $("form").validate().cancelSubmit = true;

      $("#" + _Params.SubmitButtonID + ", ." + _Params.SubmitButtonID).prop('onclick', null).off('click');
      $("#" + _Params.SubmitButtonID + ", ." + _Params.SubmitButtonID).removeClass("btn-success");
      $("#" + _Params.SubmitButtonID + ", ." + _Params.SubmitButtonID).addClass("btn-warning");
      $("#" + _Params.FormID).submit();
    }
    else
    {
      if ($("#" + _Params.FormID).valid())
      {
        $("#" + _Params.SubmitButtonID + ", ." + _Params.SubmitButtonID).prop('onclick', null).off('click');
        $("#" + _Params.SubmitButtonID + ", ." + _Params.SubmitButtonID).removeClass("btn-success");
        $("#" + _Params.SubmitButtonID + ", ." + _Params.SubmitButtonID).addClass("btn-warning");
        $("#" + _Params.FormID).submit();
      }
      else
      {
        if (_Params.ShowSavingDialog)
        {
          _Params.SavingDialog.Hide();
        }

        if (typeof (_Params.ValidationFailedCallBack) == "function")
        {        
          _Params.ValidationFailedCallBack();
        }
      }
    }
  }

  this.RebindValidation = function ()
  {
    //$("form").removeData("validator");
    //$("form").removeData("unobtrusiveValidation");
    //$.validator.unobtrusive.parse("form");
    var $form = $("form");
    $form.unbind();
      $form.data("validator", null);
    $.validator.unobtrusive.parse(document);
    $form.validate($form.data("unobtrusiveValidation").options);
  }

  this.RemoveEntity = function (prompt, formCollectionPrefix, divID) {
    var _bootstrapDialogSettings = new BootstrapDialogSettings();
    _bootstrapDialogSettings.DivID = "divDeleteConfirmation";
    _bootstrapDialogSettings.Title = "Delete Confirmation";

    if (prompt == "") {
      prompt = "Click OK to delete this entry"
    }
    _bootstrapDialogSettings.Content = prompt;
    _bootstrapDialogSettings.Size = "small";
    _bootstrapDialogSettings.Buttons = [
      { Label: "Yes", Class: "btn-warning", Icon: "fa fa-check", Callback: function () { DeleteConfirmationCallBack(formCollectionPrefix, divID) }, CloseOnClick: true },
      { Label: "No", Class: "btn-primary", Icon: "fa fa-close", CloseOnClick: true }
    ];
    _Params.LookupDeleteConfirmationDialog = new BootstrapDialog(_bootstrapDialogSettings);
    _Params.LookupDeleteConfirmationDialog.Show();
  };

  function DeleteConfirmationCallBack(formCollectionPrefix, divID) {
    var _entityID = formCollectionPrefix + "EntityState";
   
    if ($("#" + _entityID).val() == "Added") 
    {
      
      //We target both the id and class
        $("#" + divID).remove();
        $("." + divID).remove();
    }
    else
    {
      $("#" + _entityID).val("Deleted");

      //We target both the id and class
      $("#" + divID).hide();
        $("." + divID).hide();

      //Have to mark all the items so that the validator will ignore them.
      $("#" + divID + " :input").addClass("validator-ignore");
      $("." + divID + " :input").addClass("validator-ignore");
    }

    if (typeof (_Params.RemoveEntityCallBack) == "function")
    {        
      _Params.RemoveEntityCallBack();
    }
  }


  this.SetDirtyBit = function(id)
  {
    $("#" + id).val("True");
  }

  this.DisplayFormErrors = function(caption, errorPrefix)
  {
    if ($("#" + _Params.FormID).valid() == false) {

      var _errors = "";
      var _errorCount = $("#" + _Params.FormID).validate().errorList.length;
      for (var i = 0; i < _errorCount; i++) {
        _errors += $("#" + _Params.FormID).validate().errorList[i].message + "<br>";
      }

      SurgeOneAlert(caption, errorPrefix + "<br><br>" + _errors);
      return false;
    }
  }


  //Load Dependencies  
  //ClassLoader.LoadScript({ Script: "/js/BootstrapDialog.js" });

}