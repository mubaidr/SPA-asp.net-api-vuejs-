export default {
  getAuth () {
    return JSON.parse(window.localStorage.getItem('auth'))
  },
  setAuth (auth) {
    window.localStorage.setItem('auth', JSON.stringify(auth))
  },
  getUserInfo () {
    return JSON.parse(window.localStorage.getItem('userinfo'))
  },
  setUserInfo (userinfo) {
    window.localStorage.setItem('userinfo', JSON.stringify(userinfo))
  },
  clear () {
    window.localStorage.removeItem('auth')
    window.localStorage.removeItem('userinfo')
  }
}
