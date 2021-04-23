
function ProductDetail(params) {
    var _Params = params;
    var _ObjectReference = this;

    _Params.Type = "ProductDetails";
    _Params.InstanceCount = ClassLoader.InstanceCounter();
    console.log("INIT: " + _Params.Type + " - " + _Params.InstanceCount);


    this.Init = function () {
        debugger;
        $("#imageBrowes").change(function () {
            var File = this.files;
            if (File && File[0]) {
                ReadImage(File[0]);
            }
        })
    }


    var ReadImage = function (file) {
        var reader = new FileReader;
        var image = new Image;
        reader.readAsDataURL(file);
        reader.onload = function (_file) {

            image.src = _file.target.result;
            image.onload = function () {

                var height = this.height;
                var width = this.width;
                var type = file.type;
                var size = ~~(file.size / 1024) + "KB";

                $("#targetImg").attr('src', _file.target.result);
                $("#description").text("Size:" + size + ", " + height + "X " + width + ", " + type + "");
                $("#imgPreview").show();
            }
        }
    }

    var ClearPreview = function () {
        $("#imageBrowes").val('');
        $("#description").text('');
        $("#imgPreview").hide();
    }



    //Post Add Form
   

    //Loaded Dependencies
    //Extends the base class    
    ClassLoader.LoadScript({
        Script:"/js/Base/BaseEntityView.js",
        ScriptLoadedCallback: function (params) {
            _ObjectReference.prototype = new BaseEntityView(_Params);
            _ObjectReference.RebindValidation = _ObjectReference.prototype.RebindValidation;
            _ObjectReference.RemoveEntity = _ObjectReference.prototype.RemoveEntity;
        }
    });
}