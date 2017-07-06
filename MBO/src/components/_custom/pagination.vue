<template>
  <div>
    <md-progress class="md-accent" :class="{'hidden': !loading}" md-indeterminate></md-progress>
    <md-toolbar class="md-dense md-transparent">
      <md-button class="md-icon-button md-primary md-dense" @click.native="firstPage" :disabled="paging.page == 1 || disableControl">
        <md-tooltip md-direction="top">First Page</md-tooltip>
        <md-icon>first_page</md-icon>
      </md-button>
      <md-button class="md-icon-button md-primary md-dense" @click.native="previousPage" :disabled="paging.page == 1 || disableControl">
        <md-tooltip md-direction="top">Previous Page</md-tooltip>
        <md-icon>chevron_left</md-icon>
      </md-button>
      <span>&nbsp; Page {{paging.page}}/{{lastpage}} </span>
      <md-button class="md-icon-button md-primary md-dense" @click.native="nextPage" :disabled="paging.page == lastpage || disableControl">
        <md-tooltip md-direction="top">Next Page</md-tooltip>
        <md-icon>chevron_right</md-icon>
      </md-button>
      <md-button class="md-icon-button md-primary md-dense" @click.native="lastPage" :disabled="paging.page == lastpage || disableControl">
        <md-tooltip md-direction="top">Last Page</md-tooltip>
        <md-icon>last_page</md-icon>
      </md-button>
      <span style="flex: 1"></span>
      <md-input-container md-theme="default">
        <md-tooltip md-direction="top">Search</md-tooltip>
        <md-input v-model="paging.filter" @change="search" placeholder="Search"></md-input>
      </md-input-container>
      <md-button class="md-icon-button md-primary md-dense" @click.native="refresh" :disabled="loading">
        <md-tooltip md-direction="top">Refresh</md-tooltip>
        <md-icon>refresh</md-icon>
      </md-button>
    </md-toolbar>
  </div>
</template>
<script>
import { debounce } from 'lodash'

export default {
  name: 'pagination',
  props: ['lastpage', 'count', 'loading'],
  data () {
    return {
      paging: {
        page: 1,
        filter: ''
      }
    }
  },
  watch: {
    'lastpage' (val) {
      if (val === 0) {
        this.lastpage = 1
      }
    },
    'paging.page' () {
      this.refresh()
    }
  },
  computed: {
    disableControl () {
      return this.loading || !this.count
    }
  },
  methods: {
    search () {
      this.paging.page = 1
      this.refresh()
    },
    nextPage () {
      this.paging.page += 1
    },
    previousPage () {
      this.paging.page -= 1
    },
    firstPage () {
      this.paging.page = 1
    },
    lastPage () {
      this.paging.page = this.lastpage
    },
    refresh: debounce(function () {
      this.$emit('refresh', this.paging)
    }, 500, {
      leading: false,
      trailing: true
    })
  }
}

</script>
<style scoped>

</style>
