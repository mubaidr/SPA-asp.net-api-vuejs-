function test() {
  alert('test succesfull!');
}

window.onload = function () {
  console.log('Bootstrap loaded: ' + (typeof $().modal == 'function'));
};
