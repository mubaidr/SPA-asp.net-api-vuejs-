export default {
  getAuth() {
    return JSON.parse(localStorage.getItem('auth'));
  },
  setAuth(auth) {
    localStorage.setItem('auth', JSON.stringify(auth));
  },
  getUserInfo() {
    return JSON.parse(localStorage.getItem('userinfo'));
  },
  setUserInfo(userinfo) {
    localStorage.setItem('userinfo', JSON.stringify(userinfo));
  },
  clear() {
    localStorage.removeItem('auth');
    localStorage.removeItem('userinfo');
  }
};
