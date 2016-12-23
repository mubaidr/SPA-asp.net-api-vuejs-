import Vue from 'vue'
import VeeValidate from 'vee-validate';

const config = {
  errorBagName: 'errors', // change if property conflicts.
  fieldsBagName: 'fields',
  delay: 0,
  locale: 'en',
  dictionary: null,
  strict: true
};

Vue.use(VeeValidate)
