<template>
  <transition appear>
    <router-view></router-view>
  </transition>
</template>
<script>
export default {
  data () {
    return {
      transitionName: 'slide-up'
    }
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
