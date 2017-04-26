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
  import Header from './Layout/Header'
  import Footer from './Layout/Footer'

  export default {
    data () {
      return {
        transitionName: 'slide-up'
      }
    },
    components: {
      'app-footer': Footer,
      'app-header': Header
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
<style scoped="">

</style>
