import Vue from 'vue'
import Vuex from 'vuex'
import session from 'utilities/session'

Vue.use(Vuex)

const state = {
  auth: session.getAuth(),
  userinfo: session.getUserInfo(),
  settings: {
    taskView: {
      sort: [{
        name: 'Priority',
        icon: 'keyboard_arrow_down',
        type: '',
        enabled: false
      },
      {
        name: 'Due Time',
        icon: 'keyboard_arrow_down',
        type: '',
        enabled: true
      }
      ],
      filter: [{
        name: 'Users',
        icon: 'keyboard_arrow_down',
        type: '',
        enabled: false
      },
      {
        name: 'Categories',
        icon: 'keyboard_arrow_down',
        type: '',
        enabled: false
      }
      ]
    }
  }
}

const mutations = {
  setAuthentication (state, auth) {
    state.auth = auth
    session.setAuth(auth)
  },
  setUserInfo (state, userinfo) {
    state.userinfo = userinfo
    session.setUserInfo(userinfo)
  },
  removeAuthentication (state) {
    state.auth = null
    state.userinfo = null
    session.clear()
  },
  setViewMode (state, view) {
    if (view.type) {
      state.settings.view.type = view.type
    }
  }
}

const actions = {}

const getters = {
  isAuhtenticated (state) {
    return state.auth !== null && typeof state.auth !== 'undefined'
  },
  getUserInfo (state) {
    return state.userinfo
  },
  getAuth (state) {
    return state.auth
  },
  getViewMode () {
    return state.settings.view
  },
  getSettings () {
    return state.settings
  }
}

const store = new Vuex.Store({
  state,
  mutations,
  actions,
  getters
})

export default store

// todo add qoates with different categories to display with empty data, error state or waiting screens!
