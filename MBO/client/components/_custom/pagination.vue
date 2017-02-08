<template>
  <md-layout>
    <md-layout md-hide-small></md-layout>
    <md-layout>
      <md-whiteframe md-tag="section" class="full-width">
        <md-toolbar class="md-dense md-transparent">
          <!--TODO fix search-->
          <div class="md-toolbar-container">
            <md-button class="md-icon-button" @click.native="firstPage" :disabled="paging.page == 1 || loading">
              <md-tooltip md-direction="top">First Page</md-tooltip>
              <md-icon>first_page</md-icon>
            </md-button>
            <md-button class="md-icon-button" @click.native="previousPage" :disabled="paging.page == 1 || loading">
              <md-tooltip md-direction="top">Previous Page</md-tooltip>
              <md-icon>chevron_left</md-icon>
            </md-button>
            <span>&nbsp; Page {{paging.page}} of {{lastpage}} &nbsp;</span>
            <md-button class="md-icon-button" @click.native="nextPage" :disabled="paging.page == lastpage || loading">
              <md-tooltip md-direction="top">Next Page</md-tooltip>
              <md-icon>chevron_right</md-icon>
            </md-button>
            <md-button class="md-icon-button" @click.native="lastPage" :disabled="paging.page == lastpage || loading">
              <md-tooltip md-direction="top">Last Page</md-tooltip>
              <md-icon>last_page</md-icon>
            </md-button>
            <span style="flex: 1"></span>
            <md-button class="md-icon-button" @click.native="activate_search" :disabled="loading">
              <md-tooltip md-direction="top">Search</md-tooltip>
              <md-icon>search</md-icon>
            </md-button>
            <md-menu md-direction="bottom left" md-size="3">
              <md-button md-menu-trigger class="md-icon-button" :disabled="loading">
                <md-tooltip md-direction="top">Apply sorting</md-tooltip>
                <md-icon>sort</md-icon>
              </md-button>
              <md-menu-content>
                <md-subheader>
                  <span>Sort By</span>
                </md-subheader>
                <md-menu-item v-for="sort in settings.task_view.sort" :disabled="sort.enabled">
                  <span>{{sort.name}} {{sort.type}}</span>
                  <md-icon>{{sort.icon}}</md-icon>
                </md-menu-item>
              </md-menu-content>
            </md-menu>
          </div>
        </md-toolbar>
        <md-progress class="md-accent" md-indeterminate v-show="loading"></md-progress>
      </md-whiteframe>
    </md-layout>
    <md-layout md-hide-small></md-layout>
  </md-layout>
</template>
<script>
  export default {
    name: 'pagination',
    props: ['lastpage', 'count', 'loading'],
    data: function () {
      return {
        paging: {
          page: 1,
          orderby: '',
          filter: ''
        },
        search_options: {
          active: false
        }
      }
    },
    watch: {
      'lastpage': function (val) {
        if (val == 0) {
          this.lastpage = 1;
        }
      },
      'paging.page': function () {
        this.search();
      },
      'paging.orderby': function () {
        this.search();
      },
      'paging.filter': function () {
        this.search();
      }
    },
    computed: {
      settings: function () {
        return this.$store.getters.getSettings;
      }
    },
    methods: {
      search: function () {
        const _self = this;
        _self.$emit('refresh', _self.paging);
      },
      nextPage: function () {
        this.$set(this.paging, 'page', this.paging.page + 1);
      },
      previousPage: function () {
        this.$set(this.paging, 'page', this.paging.page - 1);
      },
      firstPage: function () {
        this.$set(this.paging, 'page', 1);
      },
      lastPage: function () {
        this.$set(this.paging, 'page', this.lastpage);
      },
      activate_search: function () {
        this.search_options.active = !this.search_options.active;
      }
    },
    mounted: function () {
      this.$material.inkRipple = false;
    }
  }

</script>
<style scoped></style>
