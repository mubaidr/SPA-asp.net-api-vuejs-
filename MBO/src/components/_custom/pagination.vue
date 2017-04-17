<template>
  <md-layout>
    <md-layout md-hide-small :class="{'hide': fullWidth == true}"></md-layout>
    <md-layout md-flex-medium="100">
      <md-whiteframe md-tag="section" class="full-width margin-bottom" md-elevation="0">
        <md-toolbar class="md-dense md-transparent">
          <div class="md-toolbar-container">
            <md-button md-hide-small class="md-icon-button md-dense" @click.native="firstPage" :disabled="paging.page == 1 || loading">
              <md-tooltip md-direction="top">First Page</md-tooltip>
              <md-icon>first_page</md-icon>
            </md-button>
            <md-button class="md-icon-button md-dense" @click.native="previousPage" :disabled="paging.page == 1 || loading">
              <md-tooltip md-direction="top">Previous Page</md-tooltip>
              <md-icon>chevron_left</md-icon>
            </md-button>
            <span> Page {{paging.page}}/{{lastpage}} </span>
            <md-button class="md-icon-button md-dense" @click.native="nextPage" :disabled="paging.page == lastpage || loading">
              <md-tooltip md-direction="top">Next Page</md-tooltip>
              <md-icon>chevron_right</md-icon>
            </md-button>
            <md-button md-hide-small class="md-icon-button md-dense" @click.native="lastPage" :disabled="paging.page == lastpage || loading">
              <md-tooltip md-direction="top">Last Page</md-tooltip>
              <md-icon>last_page</md-icon>
            </md-button>
            <span style="flex: 1"></span>
            <md-input-container md-inline>
              <md-tooltip md-direction="top">Search</md-tooltip>
              <md-input v-model="paging.filter" @change="search" placeholder="Search"></md-input>
            </md-input-container>
            <md-menu md-direction="bottom left" md-size="5" v-show="sortMenu">
              <md-button md-menu-trigger class="md-icon-button md-dense" :disabled="loading">
                <md-tooltip md-direction="top">Sort</md-tooltip>
                <md-icon>sort</md-icon>
              </md-button>
              <md-menu-content>
                <md-subheader>
                  <span>Sort By</span>
                </md-subheader>
                <md-menu-item v-for="sort in settings.taskView.sort" :disabled="sort.enabled">
                  <span>{{sort.name}} {{sort.type}}</span>
                  <md-icon>{{sort.icon}}</md-icon>
                </md-menu-item>
              </md-menu-content>
            </md-menu>
            <md-menu md-direction="bottom left" md-size="5">
              <md-button md-menu-trigger class="md-icon-button md-dense" :disabled="loading" v-show="viewMenu">
                <md-tooltip md-direction="top">View</md-tooltip>
                <md-icon>dashboard</md-icon>
              </md-button>
              <md-menu-content>
                <md-subheader>
                  <span>Change View</span>
                </md-subheader>
                <md-menu-item v-for="view in settings.taskView.view" :disabled="view.enabled" @click.native="changeView">
                  <span>{{view.name}}</span>
                  <md-icon>{{view.icon}}</md-icon>
                </md-menu-item>
              </md-menu-content>
            </md-menu>
            <md-button class="md-button md-dense" :disabled="loading" @click.native="refresh">
              <md-tooltip md-direction="top">Refresh</md-tooltip>
              <md-icon>refresh</md-icon>
              <span>Refresh</span>
            </md-button>
          </div>
        </md-toolbar>
        <md-progress class="md-accent" :class="{ hidden: !loading }" md-indeterminate></md-progress>
      </md-whiteframe>
    </md-layout>
    <md-layout md-hide-small :class="{'hide': fullWidth == true}"></md-layout>
  </md-layout>
</template>
<script>
  import _ from 'lodash'
  export default {
    name: 'pagination',
    props: ['lastpage', 'count', 'loading', 'view-menu', 'full-width', 'sort-menu', 'refresh-menu'],
    data () {
      return {
        paging: {
          page: 1,
          orderby: 'DueTimeDown',
          filter: ''
        },
        searchOptions: {
          active: false
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
      },
      'paging.orderby' () {
        this.refresh()
      }
    },
    computed: {
      settings () {
        return this.$store.getters.getSettings
      }
    },
    methods: {
      search () {
        const _self = this
        _self.$set(_self.paging, 'page', 1)
        _self.refresh()
      },
      refresh: _.debounce(function () {
        this.$emit('refresh', this.paging)
      }, 500, {
        leading: false,
        trailing: true
      }),
      nextPage () {
        this.$set(this.paging, 'page', this.paging.page + 1)
      },
      previousPage () {
        this.$set(this.paging, 'page', this.paging.page - 1)
      },
      firstPage () {
        this.$set(this.paging, 'page', 1)
      },
      lastPage () {
        this.$set(this.paging, 'page', this.lastpage)
      },
      changeView () {
        this.$store.commit('toggleViewMode')
      }
    },
    mounted () {
      // this.$material.inkRipple = false;
    }
  }

</script>
<style scoped>
  .hidden {
    visibility: hidden;
  }
  
  .md-input-container {
    max-width: 180px;
  }
  
  .margin-bottom {
    margin-bottom: 20px;
  }

</style>
