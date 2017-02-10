<template>
  <div>
    <span class="md-display-1">{{Task.Title}}</span>
    <p>
      <router-link class="md-accent" :to="{path: '/tasks'}">
        View All Tasks
      </router-link>
    </p>
  </div>
</template>
<script>
  import {
    getDetails
  } from 'services/tasks';

  export default {
    data: function () {
      return {
        Task: {
          Title: 'loading...'
        },
        Type: 'view',
        loading: true
      }
    },
    watch: {},
    methods: {
      retry: function () {
        const _self = this;
        _self.$refs.snackbar.close();
        window.setTimeout(function () {
          _self.loadDetails();
        }, 500);
      },
      loadDetails: function () {
        const _self = this;
        _self.$set(_self, 'loading', true);
        getDetails({
          id: _self.Id
        }).then(res => {
          _self.$set(_self, 'Task', res.data);
          _self.$set(_self, 'loading', false);
        }).catch(err => {
          _self.$set(_self, 'loading', false);
        });
      }
    },
    mounted: function () {
      const _self = this;
      _self.$set(_self, 'Type', _self.$route.params.Type);
      _self.$set(_self, 'Task', _self.$route.params.Task);
      console.log(_self.$route.params);
      //TODO fetch comments, progress history
    }
  }

</script>
<style></style>
