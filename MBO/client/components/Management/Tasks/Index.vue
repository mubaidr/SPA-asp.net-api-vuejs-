<template>
  <transition :name="transitionName" mode="out-in">
    <router-view></router-view>
  </transition>
</template>
<script>
  export default {
    data: function () {
      return {
        transitionName: 'slide-up'
      }
    },
    watch: {
      '$route' (to, from) {
        console.log(to, from);
        const toDepth = to.path.split('/').length;
        const fromDepth = from.path.split('/').length;
        if (toDepth == fromDepth) {
          this.transitionName = 'slide-up';
        } else {
          this.transitionName = toDepth < fromDepth ? 'slide-right' : 'slide-left'
        }
      }
    }
  }

</script>
<style scoped>
  /*Route change animations*/
  
  .slide-up-enter-active {
    transition: all .25s ease-out;
  }
  
  .slide-up-leave-active {
    transition: all .1s ease;
  }
  
  .slide-up-enter {
    transform: translateY(-50px);
    opacity: 0;
  }
  
  .slide-up-leave {
    transform: translateY(-20px);
    opacity: 0;
  }
  /*Test right*/
  
  .slide-right-enter-active {
    transition: all .25s ease-out;
  }
  
  .slide-right-leave-active {
    transition: all .1s ease;
  }
  
  .slide-right-enter {
    transform: translateX(-50px);
    opacity: 0;
  }
  
  .slide-right-leave-active {
    transform: translateX(20px);
    opacity: 0;
  }
  /*Test left*/
  
  .slide-left-enter-active {
    transition: all .25s ease-out;
  }
  
  .slide-left-leave-active {
    transition: all .1s ease;
  }
  
  .slide-left-enter {
    transform: translateX(50px);
    opacity: 0;
  }
  
  .slide-left-leave-active {
    transform: translateX(-20px);
    opacity: 0;
  }

</style>
