import Vue from 'vue'
import VueMaterial from 'vue-material'

Vue.use(VueMaterial)

Vue.material.registerTheme({
  default: {
    primary: 'indigo',
    accent: 'pink',
    warn: 'red',
    background: 'white'
  },
  invert: {
    primary: 'pink',
    accent: 'indigo'
  },
  'theme-danger': {
    primary: 'red'
  },
  'theme-warn': {
    primary: 'deep-orange'
  },
  'theme-normal': {
    primary: 'orange'
  },
  'theme-primary': {
    primary: 'lime'
  },
  'theme-success': {
    primary: 'green'
  }
})
