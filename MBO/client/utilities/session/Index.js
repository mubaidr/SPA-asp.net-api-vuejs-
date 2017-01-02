export default {
  get: function () {
    return JSON.parse(localStorage.getItem('auth'));
  },
  set: function (auth) {
    localStorage.setItem('auth', JSON.stringify(auth));
  },
  clear: function () {
    localStorage.removeItem('auth');
  }
};
