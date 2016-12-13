function test() {
  alert('test succesfull!');
}

window.onload = function () {
  console.log('jQuery loaded: ' + (typeof $ !== 'undefined'));
  console.log('Bootstrap loaded: ' + (typeof $().modal == 'function'));
};
