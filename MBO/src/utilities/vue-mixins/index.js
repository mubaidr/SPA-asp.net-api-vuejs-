import Vue from 'vue'

var mixin = {
  methods: {
    setErrorDetails (err) {
      // TODO use store for state tracking and to display snackbars
      if (err) {
        this.state.title = err.message
        // TODO try catch to check val
        if (err.response && err.response.data && err.response.data.error_description) {
          this.state.details = err.response.data.error_description
        }
      } else {
        this.state.title = null
      }
    }
  }
}

Vue.mixin(mixin)
