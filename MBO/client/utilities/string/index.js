String.prototype.toProperCase = function (convertCamelCase) {
  var words = this.split(' ');

  var proper = [];
  for (var index in words) {
    if (convertCamelCase) {
      proper.push(words[index].replace(/([A-Z])/g, ' $1')
        .replace(/^./, function (str) {
          return str.toUpperCase();
        }));
    } else {
      proper.push(words[index][0].toUpperCase() + words[index].slice(1));
    }
  }
  return proper.join(' ');
}
