/*
  _Params = 
  {
    SearchURL: null,
    SearchFormID: null,
    SearchResultDivID: null,
    SearchReloadCompletedCallback: null;
  }
*/
function BaseEntityList(params) {

    var _Params = params;
    var _ObjectReference = this;

    _Params.Type = "BaseEntityList";
    _Params.InstanceCount = ClassLoader.InstanceCounter();
    console.log("INIT: " + _Params.Type + " - " + _Params.InstanceCount);

    this.ReloadSearch = function () {
        debugger;
        $.ajax({
            type: "POST",
            url: _Params.SearchURL,
            data: $("#" + _Params.SearchFormID).serialize(),
            success: function (data) {
                $("#" + _Params.SearchResultDivID).html(data);

                if (_Params.SearchReloadCompletedCallback != null &&
                    typeof _Params.SearchReloadCompletedCallback == "function") {
                    _Params.SearchReloadCompletedCallback();
                }
            }
        });
    }

    this.Search = function () {
        $("#Page").val(1);
        this.ReloadSearch();
    }

    this.SetPage = function (page) {
        if (page < 1) { return; }
        $("#Page").val(page);
        this.ReloadSearch();
    }

    this.SetSortOrder = function (sortColumn) {
        if ($("#SortColumn").val() == sortColumn) {
            if ($("#SortOrder").val() == "Asc" ||
                $("#SortOrder").val() == "asc") {
                $("#SortOrder").val("Desc");
            }
            else {
                $("#SortOrder").val("Asc");
            }
        }
        else {
            $("#SortColumn").val(sortColumn);
            $("#SortOrder").val("Asc");
        }
        this.ReloadSearch();
    }

    this.ToggleCheckBoxes = function (event, cssClass, checked, autosearch) {
        CancelClickEvent(event);
        $('.' + cssClass).prop('checked', checked);

        if (autosearch) {
            _ObjectReference.Search();
        }
    }


}