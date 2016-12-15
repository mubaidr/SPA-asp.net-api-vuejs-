var formValidation = function () {
  var messages = [];
  for (var prop in this.$v.credentials) {
    if (typeof this.credentials[prop] != 'undefined') {
      var obj = this.$v.credentials[prop];
      if (obj.$error) {
        for (var type in obj) {
          if (!type.includes('$') && !obj[type]) {
            //messages.push(obj);
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
            }
          }
        }
      }
    }
  }
  return {
    isInvalid: this.$v.validationGroup.$invalid,
    messages: messages
  }
}

module.exports = {
  formValidation: formValidation
}
