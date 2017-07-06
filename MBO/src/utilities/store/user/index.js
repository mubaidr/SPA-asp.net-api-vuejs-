import session from 'utilities/session'
import account from 'services/account'

console.log(account)

export default {
  state: {
    auth: session.getAuth(),
    userInfo: session.getUserInfo()
  },
  mutations: {
    setAuthentication (state, auth) {
      state.auth = auth
      session.setAuth(auth)
    },
    setUserInfo (state, userInfo) {
      state.userInfo = userInfo
      session.setUserInfo(userInfo)
    },
    removeAuthentication (state) {
      state.auth = null
      state.userInfo = null
      session.clear()
    }
  },
  actions: {},
  getters: {
    isAuthenticated (state) {
      return state.auth !== null && typeof state.auth !== 'undefined'
    },
    getUserInfo (state) {
      return state.userInfo
    },
    getAuth (state) {
      return state.auth
    }
  }
}
