<template>
  <v-dialog v-model="dialog" activator="parent" width="auto">
    <v-card>
      <v-container>
        <v-table>
          <thead>
            <tr>
              <th class="text-left">Reference No</th>
              <th class="text-left">Gross Weight</th>
              <th class="text-left">Tare Weight</th>
              <th class="text-left">Nett Weight</th>
              <th class="text-left">Incoming Date Time</th>
              <th class="text-left">Created At</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td v-text="bundle_info.BatchNo"></td>
              <td v-text="bundle_info.GrossWeight"></td>
              <td v-text="bundle_info.TareWeight"></td>
              <td v-text="bundle_info.NettWeight"></td>
              <td v-text="bundle_info.IncomingDateTime"></td>
              <td v-text="bundle_info.CreatedAt"></td>
            </tr>
          </tbody>
        </v-table>
      </v-container>
      <v-card-actions>
        <v-row>
          <v-col>
            <v-btn color="primary" block @click="close_dialog(1)"
              >Confirm</v-btn
            >
          </v-col>
          <v-col>
            <v-btn color="danger" block @click="close_dialog(0)">Cancel</v-btn>
          </v-col>
        </v-row>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  name: "bundle-info-component",
  data() {
    return {
      dialog: true,
    };
  },
  props: {
    bundle_info: null,
    bundle_info_toggle: null,
  },
  computed: {},
  methods: {
    close_dialog(is_confirmed) {
      if (is_confirmed || is_confirmed === 1) {
        this.$emit("confirm_outbound");
        this.dialog = false;
      }
    },
  },
};
</script>

<!-- @click="dialog = false" -->
