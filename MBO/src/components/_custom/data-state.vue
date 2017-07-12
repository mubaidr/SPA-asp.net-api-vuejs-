<template>
  <div class="flex-vertical min-height full-width text-center">
    <div class="no-content">
      <md-icon class="md-size-3x" :class="icon_class">cloud_queue</md-icon>
      <br>
      <span v-if="loading">Loading...</span>
      <span v-else>Empty!</span>
      <p>{{quote}}</p>
    </div>
  </div>
</template>

<script>
export default {
  name: 'data-state',
  props: ['loading'],
  data () {
    return {
      quote: ''
    }
  },
  watch: {
    'loading' () {
      this.quote = this.getQuote()
    }
  },
  computed: {
    icon_class () {
      if (this.loading) {
        return 'md-primary'
      } else if (this.error) {
        return 'md-warn'
      }
      return ''
    }
  },
  methods: {
    getQuote () {
      if (this.loading) {
        return this.$store.getters.getQuoteLoading
      } else {
        return this.$store.getters.getQuoteEmpty
      }
    }
  },
  created () {
    this.quote = this.getQuote()
  }
}
</script>

<style>

</style>
