<template>
  <div>
    <app-header></app-header>
    <div class="container">
      <transition name="slide-fade" mode="out-in">
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
    components: {
      'app-footer': Footer,
      'app-header': Header
    }
  }

</script>
<style scoped>
  /*Route change animations*/
  
  .slide-fade-enter-active {
    transition: all .25s ease-out;
  }
  
  .slide-fade-leave-active {
    transition: all .1s ease;
  }
  
  .slide-fade-enter {
    transform: translateY(-100px);
    opacity: 0;
  }
  
  .slide-fade-leave-active {
    transform: translateY(20px);
    opacity: 0;
  }

</style>
