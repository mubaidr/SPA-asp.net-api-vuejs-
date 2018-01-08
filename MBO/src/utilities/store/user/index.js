/* eslint-disable no-param-reassign */
// eslint-disable-next-line
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
      return account.signup(credentials)
    },
    signin (context, credentials) {
      return account.signin(credentials).then(resAuth => {
        context.commit('setAuthentication', resAuth.data)
        account.getUserInfo().then(res => {
          context.commit('setUserInfo', res.data)
        })
      })
    },
    signout (context) {
      account
        .signout()
        .then(() => {
          context.commit('removeAuthentication')
        })
        .catch(() => {})
    },
    getUserInfo (context) {
      account
        .getUserInfo()
        .then(res => {
          context.commit('setUserInfo', res.data)
        })
        .catch(() => {})
    },
    getUserList () {
      return account.getUserList()
    }
  },
  getters: {
    getAuth (state) {
      return state.auth
    },
    isAuthenticated (state) {
      return state.auth !== null && typeof state.auth !== 'undefined'
    },
    userInfo (state) {
      return state.userInfo
    }
  }
}
