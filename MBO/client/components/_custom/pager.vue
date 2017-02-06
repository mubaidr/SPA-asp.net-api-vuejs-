<template>
  <div><br/>
    <md-whiteframe md-tag="section" md-elevation="1">
      <md-toolbar>
        <span style="flex: 1"></span>
        <md-button class="md-icon-button md-mini" @click="firstPage" :disabled="paging.page == 1">
          <md-icon>first_page</md-icon>
        </md-button>
        <md-button class="md-icon-button md-mini" @click="previousPage" :disabled="paging.page == 1">
          <md-icon>chevron_left</md-icon>
        </md-button>
        <span>&nbsp; Page {{paging.page}} of {{lastpage}} &nbsp;</span>
        <md-button class="md-icon-button" @click="nextPage" :disabled="paging.page == lastpage">
          <md-icon>chevron_right</md-icon>
        </md-button>
        <md-button class="md-icon-button" @click="lastPage" :disabled="paging.page == lastpage">
          <md-icon>last_page</md-icon>
        </md-button>
        <span style="flex: 1"></span>
        <md-input-container md-inline v-show="search_options.active">
          <md-tooltip md-direction="top">Search using title, description, user etc</md-tooltip>
          <label>Search</label>
          <md-input v-model="paging.filter"></md-input>
        </md-input-container>
        <md-button class="md-icon-button" @click="activate_search">
          <md-icon>search</md-icon>
        </md-button>
        <md-menu md-direction="bottom left" md-size="3">
          <md-button md-menu-trigger class="md-icon-button">
            <md-tooltip md-direction="top">Apply filter</md-tooltip>
            <md-icon>sort</md-icon>
          </md-button>
          <md-menu-content>
            <md-menu-item disabled>Sort By</md-menu-item>
            <md-menu-item v-for="sort in settings.task_view.sort" :disabled="sort.enabled">
              <span>{{sort.name}} {{sort.type}}</span>
              <md-icon>{{sort.icon}}</md-icon>
            </md-menu-item>
          </md-menu-content>
        </md-menu>
      </md-toolbar>
    </md-whiteframe>
    <br/></div>
</template>
<script>
  export default {
    name: 'pager',
    props: ['lastpage', 'count'],
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
        if (this.paging.page < this.lastpage) {
          this.$set(this.paging, 'page', this.paging.page + 1);
          this.search();
        }
      },
      previousPage: function () {
        if (this.paging.page > 1) {
          this.$set(this.paging, 'page', this.paging.page - 1);
          this.search();
        }
      },
      firstPage: function () {
        this.$set(this.paging, 'page', 1);
        this.search();
      },
      lastPage: function () {
        this.$set(this.paging, 'page', this.lastpage);
        this.search();
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
