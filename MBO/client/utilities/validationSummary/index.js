module.exports = {
  Summary: function () {
    var _self = this;
    var _isValid = true;
    var _validationData = _self.$v;
    var _componentData = _self._data;
    var _validationGroupEnabled = false;

    var isformValid = true;
    var messages = [];

    //TODO Compare _validationData root with _componentData for group name

    var _getMessages = function (obj, name) {
      var __isDirtyField = obj['$dirty'];
      var __isErrorField = obj['$error'];
      var __isInvalidField = obj['$invalid'];

      for (var prop in obj) {
        var __val = obj[prop];
        var __type = typeof __val;

        if (__type === 'object') {
          _getMessages(__val, prop);
        } else {
          var __isBuiltInProp = prop.includes('$') && __type === 'boolean';

          if (!__val && !__isBuiltInProp && __isDirtyField && __isInvalidField) {
            //TODO Only specific validation group to be marked
            isformValid = false;

            switch (prop) {
              case 'required':
                messages.push(name.toProperCase() + ' field is required.');
                break;
              case 'alphaNum':
                messages.push(name.toProperCase() + ' field can only contain alpha-numeric characters.');
                break;
              case 'sameAs':
                messages.push(name.toProperCase() + ' field must match with Password.');
                break;
              case 'minLength':
                messages.push(name.toProperCase() + ' field must contain atleast 6 characters.');
                break;
              default:
                messages.push(name.toProperCase() + ' field is not valid.');
                break;
            }
          }
        }
      }
    };

    _getMessages(_validationData);
    //console.log(_validationData, isformValid);

    return {
      hasError: !isformValid,
      messages: messages
    }
  }
}
