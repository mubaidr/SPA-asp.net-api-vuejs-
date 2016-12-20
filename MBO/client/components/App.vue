<template>
  <div>
    <app-header></app-header>
    <div class="container">
      <transition name="slide-fade" mode="out-in">
        <router-view></router-view>
      </transition>
      <app-footer></app-footer>
    </div>
  </div>
</template>
<script>
  import Header from './Header'
  import Footer from './Footer'

  export default {
    components: {
      'app-footer': Footer,
      'app-header': Header
    }
  }

</script>
