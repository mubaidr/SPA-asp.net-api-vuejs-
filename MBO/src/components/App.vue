<template>
  <div>
    <app-header></app-header>
    <div class="container">
      <transition appear :name="transitionName" mode="out-in">
        <router-view></router-view>
      </transition>
    </div>
    <app-footer></app-footer>
  </div>
</template>
<script>
import appHeader from './Layout/Header'
import appFooter from './Layout/Footer'

export default {
  data () {
    return {
      transitionName: 'slide-up'
    }
  },
  components: {
    appFooter,
    appHeader
  },
  watch: {
    '$route' (to, from) {
      const toDepth = to.path.split('/').length
      const fromDepth = from.path.split('/').length
      if (toDepth === fromDepth) {
        this.transitionName = 'slide-up'
      } else {
        this.transitionName = toDepth < fromDepth ? 'slide-right' : 'slide-left'
      }
    }
  }
}

</script>
<style scoped>

</style>
