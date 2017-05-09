import Vue from 'vue'

var mixin = {
  methods: {
    setErrorDetails (error) {
      console.dir(error)
    }
  }
}

Vue.mixin(mixin)
