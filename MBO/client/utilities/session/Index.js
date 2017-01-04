export default {
  getAuth: function () {
    return JSON.parse(localStorage.getItem('auth'));
  },
  setAuth: function (auth) {
    localStorage.setItem('auth', JSON.stringify(auth));
  },
  getUserInfo: function () {
    return JSON.parse(localStorage.getItem('userinfo'));
  },
  setUserInfo: function (userinfo) {
    localStorage.setItem('userinfo', JSON.stringify(userinfo));
  },
  clear: function () {
    localStorage.removeItem('auth');
    localStorage.removeItem('userinfo');
  }
};
