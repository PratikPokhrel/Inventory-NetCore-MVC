//Namespace
var Inventory = {};

//The two __ are there as there is a class name conflict with the template :<
function ClassLoader() {
    var _Params = {};
    var _ObjectReference = this;
    _Params.InstanceCount = 0;
    _Params.ScriptList = new Array();

    //http://stackoverflow.com/questions/9803947/create-object-from-string
    /*
    Sample params object
    {
        Script: "@Url.Content("~/Areas/SOAPs/Scripts/SOAPList.js")",
        ObjectName: "SOAPList",    
        InstanceName: "_SOAPList",    
        NewInstance: true:false
        InstanceParameters: {},
        Variable: _SOAPList,
        InstanceCallback: LoadedSOAPListScript
        ScriptLoadedCallback: ScriptLoadedCallback
    }
    */
    this.GetClassInstance = function (params) {
        params.ScriptLoadedCallback = _ObjectReference.InstantiateClass;
        _ObjectReference.LoadScript(params);
    }

    this.InstantiateClass = function (params) {
        //Check to make sure we InstanceParameters if we do not we create an empty object
        //All JS classes within this project require an object as a parameter
        if (params.InstanceParameters == null) {
            params.InstanceParameters = {}
        }
        //For the lazy programmer you can instantiate an instance with the same name as the object name
        if (params.InstanceName == null) {
            params.InstanceName = params.ObjectName;
        }

        if (params.NewInstance == null) {
            params.NewInstance = false;
        }

        var _tempInstance = null;
        //We create a new instance if
        //1) The user tells us to through NewInstance = true
        //2) The instance is still undefined
        if (params.NewInstance == true ||
            typeof Inventory[params.InstanceName] == "undefined") {
            //console.log("New instance of - '" + params.ObjectName + "' instance name '" + params.InstanceName + "'");          
            console.log("SL_INSTANTIATE_CLASS: " + params.ObjectName + " AS " + params.InstanceName);
            _tempInstance = new window[params.ObjectName](params.InstanceParameters);
            Inventory[params.InstanceName] = _tempInstance;
        }
        else {
            _tempInstance = Inventory[params.InstanceName];
        }

        //There are cases where we might end up loading a script twice -- however we only want to track it once.       
        if ($.inArray(params.Script, _Params.ScriptList) == -1) {
            _Params.ScriptList.push(params.Script);
        }

        params.Variable = _tempInstance;

        //============================================================
        //dirty hack -- we have loaded a class we hide the spinner. There is a bug that the complete is not being called
       // $('#DivInventoryAjaxCallSpinner').modal('hide');
        //============================================================

        if (params.InstanceCallback != null) {
            params.InstanceCallback(params);
        }
    }

    this.LoadScript = function (params) {
        //Check to see if the script list was loaded already  
        if ($.inArray(params.Script, _Params.ScriptList) == -1) {
            $.ajax({
                url: params.Script,
                dataType: "script",
                cache: true,
                async: true,
                success: function (data, textStatus, jqxhr) {
                    console.log("SL_LOADED: " + params.Script)
                    //There are cases where we might end up loading a script twice -- however we only want to track it once.       
                    if ($.inArray(params.Script, _Params.ScriptList) == -1) {
                        _Params.ScriptList.push(params.Script);
                    }

                    if (params.ScriptLoadedCallback != null) {
                        params.ScriptLoadedCallback(params);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    console.log("SL_ERROR: " + params.Script)
                    console.log(xhr);
                    console.log(thrownError);
                }
            })
        }
        else {
            if (params.ScriptLoadedCallback != null) {
                params.ScriptLoadedCallback(params);
            }
        }
    }

    this.InstanceCounter = function () {
        _Params.InstanceCount++;
        return _Params.InstanceCount;
    }
}