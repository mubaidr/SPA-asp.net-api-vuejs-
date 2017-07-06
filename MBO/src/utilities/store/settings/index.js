import session from 'utilities/session'

export default {
  state: {
    settings: session.getSettings(),
    quotes: session.getQuotes()
  },
  mutations: {
    toggleViewMode (state) {
      for (var i = 0; i < state.settings.taskView.view.length; i++) {
        state.settings.taskView.view[i].enabled = !state.settings.taskView.view[i].enabled
      }
      session.setSettings(state.settings)
    }
  },
  actions: {},
  getters: {
    getViewMode (state) {
      return state.settings.view
    },
    getSettings (state) {
      return state.settings
    },
    getQuoteEmpty (state) {
      let items = state.quotes.empty
      return items[Math.floor(Math.random() * items.length)]
    }
  }
}
