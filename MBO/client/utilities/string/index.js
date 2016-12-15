//Convert a string to proper case
//conver camel case to proper case
String.prototype.toProperCase = function () {
  var words = this.split(' ');

  if (words.length === 1) {

    return words[0].replace(/([A-Z])/g, ' $1')
      .replace(/^./, function (str) {
        return str.toUpperCase();
      });

  } else {

    var proper = [];
    for (var index in words) {
      proper.push(words[index][0].toUpperCase() + words[index].slice(1));
    }
    return proper.join(' ');

  }
}
