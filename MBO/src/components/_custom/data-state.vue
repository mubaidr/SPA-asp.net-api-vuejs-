<template>
  <div class="flex-vertical min-height full-width">
    <div class="no-content">
      <md-icon class="md-size-3x" :class="icon_class">cloud_queue</md-icon>
      <br>
      <span v-if="loading">Loading...</span>
      <span v-else-if="!error">Empty!</span>
      <span v-else="">Error!</span>
      <p>{{quote}}</p>
    </div>
  </div>
</template>
<script>
export default {
  name: 'data-state',
  props: ['loading', 'error'],
  data () {
    return {
      quote: ''
    }
  },
  watch: {
    'loading' () {
      this.quote = this.getQuote()
    },
    'error' () {
      this.quote = this.getQuote()
    }
  },
  computed: {
    icon_class () {
      if (this.loading) {
        return 'md-primary loading'
      } else if (this.error) {
        return 'md-warn'
      }
      return ''
    }
  },
  methods: {
    getQuote () {
      if (this.loading) {
        return this.$store.getters.getQuoteTry
      } else if (this.error) {
        return this.$store.getters.getQuoteFail
      }
      return this.$store.getters.getQuoteEmpty
    }
  },
  created () {
    this.quote = this.getQuote()
  }
}

</script>
<style scoped="">
  .no-content{
    text-align: center;
    opacity: 0.8;
  }

  .loading{
    animation-delay: 0s;
    -webkit-animation-name: jump;
    animation-name: jump;
    -webkit-animation-duration: 1s;
    animation-duration: 1s;
    -webkit-animation-fill-mode: both;
    animation-fill-mode: both;
    animation-iteration-count: infinite;
    transform-origin: 50%;
    will-change: transform;
  }

  @keyframes jump {
    0%
    {
      -webkit-transform: scale(1);
      transform: scale(1);
    }    
    50% {
      -webkit-transform: scale(1.25);
      transform: scale(1.25);
    }
    100% {
      -webkit-transform: scale(1);
      transform: scale(1);
    }
  }
</style>
