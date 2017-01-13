import Vue from 'vue';
import VueMaterial from 'vue-material';



Vue.use(VueMaterial);

Vue.material.registerTheme({
  default: {
    primary: 'indigo',
    accent: 'pink'
  },
  invert: {
    primary: 'pink',
    accent: 'indigo'
  }
});
