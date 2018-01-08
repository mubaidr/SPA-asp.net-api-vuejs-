/* eslint-disable no-param-reassign */
// eslint-disable-next-line
import session from 'utilities/session'

export default {
  state: {
    settings: session.getSettings(),
    quotes: session.getQuotes()
  },
  mutations: {
    toggleViewMode (state) {
      for (let i = 0; i < state.settings.taskView.view.length; i += 1) {
        state.settings.taskView.view[i].enabled = !state.settings.taskView.view[
          i
        ].enabled
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
      const items = state.quotes.empty
      return items[Math.floor(Math.random() * items.length)]
    },
    getQuoteLoading (state) {
      const items = state.quotes.try
      return items[Math.floor(Math.random() * items.length)]
    },
    getQuoteError (state) {
      const items = state.quotes.fail
      return items[Math.floor(Math.random() * items.length)]
    }
  }
}
