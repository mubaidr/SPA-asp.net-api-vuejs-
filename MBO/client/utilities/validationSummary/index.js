module.exports = {
  valSummary: function () {
    var _self = this;
    var messages = [];

    for (var prop in _self.$v.credentials) {
      if (typeof _self.credentials[prop] != 'undefined') {
        var obj = _self.$v.credentials[prop];
        if (obj.$error) {
          for (var type in obj) {
            if (!type.includes('$') && !obj[type]) {
              switch (type) {
                case 'required':
                  messages.push(prop.toProperCase() + ' field is required.');
                  break;
                case 'alphaNum':
                  messages.push(prop.toProperCase() + ' field can only contain alpha-numeric characters.');
                  break;
                case 'sameAs':
                  messages.push(prop.toProperCase() + ' field must match with Password.');
                  break;
                case 'minLength':
                  messages.push(prop.toProperCase() + ' field must contain atleast 6 characters.');
                  break;
                default:
                  messages.push(prop.toProperCase() + ' field is invalid.');
                  break;
              }
            }
          }
        }
      }
    }

    return {
      hasError: ((_self.$v.validationGroup && _self.$v.validationGroup.$invalid) || _self.$v.$invalid) && !!messages.length,
      messages: messages
    }
  }
}
