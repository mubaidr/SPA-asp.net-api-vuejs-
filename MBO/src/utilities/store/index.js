import Vue from 'vue'
import Vuex from 'vuex'
import session from 'utilities/session'

Vue.use(Vuex)

const state = {
  auth: session.getAuth(),
  userInfo: session.getUserInfo(),
  settings: session.getSettings(),
  quotes: session.getQuotes()
}

const mutations = {
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
  },
  toggleViewMode (state) {
    for (var i = 0; i < state.settings.taskView.view.length; i++) {
      state.settings.taskView.view[i].enabled = !state.settings.taskView.view[i].enabled
    }
    session.setSettings(state.settings)
  },
  setErrorState (err) {
    if (err) {
      this.state.title = err.message
      try {
        if (err.response.data.error_description) {
          this.state.details = err.response.data.error_description
        }
      } catch (error) {
        this.state.details = err.details ? err.details : 'Server Error!'
      }
    } else {
      this.state.title = null
    }
  }
}

const actions = {}

const getters = {
  isAuthenticated (state) {
    return state.auth !== null && typeof state.auth !== 'undefined'
  },
  getUserInfo (state) {
    return state.userInfo
  },
  getAuth (state) {
    return state.auth
  },
  getViewMode (state) {
    return state.settings.view
  },
  getSettings (state) {
    return state.settings
  },
  getQuoteEmpty (state) {
    console.log(state)
    let items = state.quotes.empty
    return items[Math.floor(Math.random() * items.length)]
  },
  getQuoteTry (state) {
    let items = state.quotes.try
    return items[Math.floor(Math.random() * items.length)]
  },
  getQuoteFail (state) {
    let items = state.quotes.fail
    return items[Math.floor(Math.random() * items.length)]
  },
  getQuoteSuccess (state) {
    let items = state.quotes.success
    return items[Math.floor(Math.random() * items.length)]
  },
  getQuoteInspire (state) {
    let items = state.quotes.inspire
    return items[Math.floor(Math.random() * items.length)]
  }
}

const store = new Vuex.Store({
  state,
  mutations,
  actions,
  getters
})

export default store
