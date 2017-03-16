<template>
  <div class="flex-vertical min-height full-width">
    <md-card class="full-width">
    </md-card>
  </div>
</template>
<script>

  export default {
    name: 'message-create',
    data () {
      return {
        Page: {
          isLoading: false
        },
        Users: [],
        Catalog: {
          Categories: [],
          Users: []
        }
        }
      },
    methods: {},
    mounted () {
      const _self = this

      getUsersList().then(res => {
        _self.$set(_self.Catalog, 'Users', res.data)
      }).catch(err => {
        console.log(err)
      })
    }
  }

</script>
<style scoped>

</style>
