import session from 'utilities/session'
import account from 'services/account'

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
  actions: {
    signup (context, credentials) {
      console.log(account.signup(credentials))
      return account.signup(credentials)
    },
    signin (context, credentials) {
      account.signin(credentials).then((res) => {
        context.commit('setAuthentication', res.data)
        account.getUserInfo().then((res) => {
          context.commit('setUserInfo', res.data)
        }).catch(() => {})
      }).catch(() => {})
    },
    signout (context) {
      account.signout().then(() => {
        context.commit('removeAuthentication')
      }).catch(() => {})
    },
    getUserInfo (context) {
      account.getUserInfo().then((res) => {
        context.commit('setUserInfo', res.data)
      }).catch(() => {})
    },
    getUserList (context) {
      return account.getUserList()
    }
  },
  getters: {
    isAuthenticated (state) {
      return state.auth !== null && typeof state.auth !== 'undefined'
    },
    userInfo (state) {
      return state.userInfo
    }
  }
}
