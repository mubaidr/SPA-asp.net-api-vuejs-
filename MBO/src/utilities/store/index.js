import Vue from 'vue'
import Vuex from 'vuex'

import settingsModule from './settings'
import userModule from './user'
import tasksModule from './tasks'
import messagesModule from './messages'

Vue.use(Vuex)

const store = new Vuex.Store({
  modules: {
    user: userModule,
    settings: settingsModule,
    tasks: tasksModule,
    messages: messagesModule
  }
})

export default store
