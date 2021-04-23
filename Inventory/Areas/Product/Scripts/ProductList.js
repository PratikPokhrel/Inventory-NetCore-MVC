function ProductList(params) {
    var _Params = params;
    console.log(_Params);
    var _ObjectReference = this;

    _Params.Type = "ProductList";
    _Params.InstanceCount = ClassLoader.InstanceCounter();
    console.log("INIT: " + _Params.Type + " - " + _Params.InstanceCount);

    //Methods dealing with the table and edit
    this.InitTable = function () {
        //InitGridEvent();
    }

    InitGridEvent = function () {
        $('table').on('click', 'i', function () {
            _ObjectReference.EditProduct($(this).data("editurl"), $(this).data("title"));
        });
    }

   


    this.Delete = function (id) {
        if (confirm("Do you want to delete this row?")) {
            $.ajax({
                //type: "POST",
                url: "/Product/Product/Delete",
                data: { id: id },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.success) {
                        $.notify(response.message, { globalposition: 'top center', className: 'success' });
                        document.location.href = "/Product/Product";
                    } else {
                        $.notify(response.message, { globalposition: 'top center', className: 'success' });
                    }
                }
            });
        }
    }
   

    //Loaded Dependencies
    //Extends the base class
    _Params.SearchURL = APPLICATION_PATH+"/Product/Product/Search";
    _Params.SearchFormID = "FormSearchProductList";
    _Params.SearchResultDivID = "divSearchResult";
    _Params.SearchReloadCompletedCallback = this.InitTable;

    ClassLoader.LoadScript({
        Script:  "/js/Base/BaseEntityList.js",
        ScriptLoadedCallback: function (params) {
            _ObjectReference.prototype = new BaseEntityList(_Params);
            _ObjectReference.ReloadSearch = _ObjectReference.prototype.ReloadSearch;
            _ObjectReference.Search = _ObjectReference.prototype.Search;
            _ObjectReference.SetPage = _ObjectReference.prototype.SetPage;
            _ObjectReference.SetSortOrder = _ObjectReference.prototype.SetSortOrder;

            //LoadOtherDepencies();
        }
    });

    function LoadOtherDepencies() {
        ClassLoader.GetClassInstance({
            Script: "/Product/Scripts/ProductDetail.js",
            ObjectName: "ProductDetail",
            InstanceName: "ProductDetail",
            InstanceParameters:
            {
                SaveProductCallBack: _ObjectReference.SaveProductCallBack
            },
            InstanceCallback: function (params) {
                _Params.ProductDetail_Handler = params.Variable;
            }
        });
    }

    this.SaveProductCallBack = function (data) {
        console.log(data, "DAta")
        $("#divSearchResult").html(data.ProductList);
        InitGridEvent();
    }
}