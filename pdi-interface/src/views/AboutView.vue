<template>
  <div>
    <button v-text="`New Batch`" />
    <button v-text="`Continue previous batch`" />
  </div>
</template>

<script>
export default {
  name: "dashboard_view",
  data() {
    return {
      is_login: null,
    };
  },
  methods: {},
};
</script>
