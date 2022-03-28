<template>
  <h5 class="q-mb-lg q-mt-none">
    {{ routeTitle }}
  </h5>

  <table-list
    :rows="rows"
    :columns="columns"
    v-model:pagination="pagination"
    :rows-per-page-options="rowsPerPageOptions"
    :show-top="true"
    row-key="id"
    @request="onPaginationRequest"
    @action-create="onActionCreate"
    @action-edit="onActionEdit"
    @action-delete="onActionDelete"
  >
    <template v-slot:top-right>
      <q-select
        class="q-mr-md filter-isenable"
        v-model="filter.filterItem"
        outlined
        dense
        :options="filterItemOptions"
        label="篩選項目"
      />
      <q-input
        class="q-mr-md"
        v-model.trim="filter.filterValue"
        outlined
        dense
        :placeholder="filter.filterItem.placeholder"
        @keypress.enter="onFilter"
      />
      <q-select
        class="q-mr-md filter-isenable"
        v-model="filter.isEnable"
        outlined
        dense
        :options="isEnableOptions"
        label="是否啟用"
      />
      <q-btn class="q-mr-md" color="secondary" label="查詢" @click="onFilter" />
      <q-btn class="q-mr-md" color="secondary" label="匯出" />
      <q-btn class="q-mr-md" color="secondary" label="清空條件" @click="onClearFilter" />
    </template>
  </table-list>

  <edit-form
    v-if="isOpened"
    v-model="isOpened"
    :is-create="isCreate"
    :fetch-item-for-edit="fetchItemForEdit"
    @update:model-value="onClearFormData"
    @submit-form="onSubmitForm"
  />
</template>

<script>
import { ref, reactive, onMounted, computed } from 'vue';
import { useRoute } from 'vue-router';
import { useStore } from 'vuex';
import dayjs from 'dayjs';
import pcPhone from '@admin-store/pc-phone'; // COPYDANIEL: batch copy  TYPE2 TYPE3

import { useQuasar } from 'quasar';
import { useSuccessNotify } from '@composables/use-notify';
import { useConfirmDialog } from '@composables/use-dialog';

import TableList from '@components/table-list';
import EditForm from './edit-form';

/**
 ********************************************************************
 * 以下為列表欄位的參數定義，相關設定方式可以參考這一頁：
 * https://quasar.dev/vue-components/table#qtable-api
 * 左邊選單選「Columns」就有了
 * 以下簡單說明常用的參數怎麼設定：
 * @param {string} name 欄位名稱，用在客製化時要抓取的欄位 id 名稱
 * @param {string} label 顯示在欄位表頭上的名稱
 * @param {string} align 欄位的對齊方式，有 left、center、right(預設值) 可以使用
 * @param {string | function} field 可參考對應 API 給的欄位 key 值名稱，可以是字串(api key)，也可以是 function(row) 進行客製化處理(常用的是有其他欄位合併成一個值顯示) row 為該列的單筆 item 資料
 * @param {function} format 自定義欄位顯示的格式，例如該欄位為時間格式，可以透過這個方式進行欄位格式轉換為 human datetime，或是金額的千分位格式化；欄位可用參數：function(val, row): val: 該欄位的值，row: 該列的整筆 item 資料
 * @param {boolean} sortable 該欄位是否可以進行排序的點選
 * 剩餘可以使用的參數可以參考上述的 URL
 ********************************************************************
 */

const columns = [
  {
    name: 'seqNo', // COPYDANIEL: Table schema column name
    label: 'Seq No.', //COPYDANIEL: desc
    align: 'left', // COPYDANIEL: integer right
    field: 'seqNo', // COPYDANIEL: Table schema column name
    sortable: true,
  },
  {
    name: 'computerName',
    label: '電腦名稱',
    align: 'left',
    field: 'computerName',
    sortable: true,
  },
  {
    name: 'computerIp',
    label: 'IP 位址',
    align: 'left',
    field: 'computerIp',
    sortable: true,
  },
  {
    name: 'extCode',
    label: '分機號碼',
    align: 'left',
    field: 'extCode',
    sortable: true,
  },
  {
    name: 'memo',
    label: '備註',
    align: 'left',
    field: 'memo',
    sortable: true,
  },
  {
    name: 'udpateDt',
    label: '異動時間',
    align: 'left',
    field: (row) => dayjs(row.updateDt).format('YYYY-MM-DD HH:mm:ss'), // COPYDANIEL: row.updateDt
    sortable: true,
  },
  {
    // COPYDANIEL: 固定的免套
    name: 'actions',
    align: 'center',
    label: '動作',
  },
];

export default {
  name: 'PcPhone', // COPYDANIEL: 大 CamelCase TYPE1

  components: {
    TableList,
    EditForm,
  },

  setup() {
    // 抓取 router meta 設定的 title
    const route = useRoute();
    const routeTitle = route.meta.title;

    /**
     ********************************************************************
     * 動態載入 Vuex store
     * 需先確定 module 是否有載入(hasModule)才 register module
     * @param moduleName 可以設定後以下用到的程式碼內皆套用
     ********************************************************************
     */
    const store = useStore();
    const moduleName = 'pcPhone'; // COPYDANIEL: store module name, same as line 63. TYPE2
    !store.hasModule(`${moduleName}`) && store.registerModule(`${moduleName}`, pcPhone); // COPYDANIEL: same as line 63. TYPE2

    const $q = useQuasar();
    const rows = ref([]);
    const isCreate = ref(true);
    const isOpened = ref(false);
    const fetchItemForEdit = ref({});

    const pagination = ref({ ...store.getters['app/getDefaultPagination'] });
    const defaultSortBy = pagination.value.sortBy;
    const rowsPerPageOptions = computed(() => store.getters['app/getRowsPerPageOptions']);

    // COPYDANIEL: 查詢欄位下拉
    const filterItemOptions = [
      {
        label: '電腦名稱',
        value: 'computerName', // COPYDANIEL: search table schema name
        placeholder: '請輸入電腦名稱',
      },
      {
        label: '電話分機',
        value: 'extCode', // COPYDANIEL: search table schema name
        placeholder: '請輸入電話分機',
      },
    ];

    const isEnableOptions = [
      {
        label: '全部',
        value: 'ALL',
      },
      {
        label: '啟用',
        value: '1',
      },
      {
        label: '停用',
        value: '0',
      },
    ];

    const filter = reactive({
      filterItem: filterItemOptions[0],
      filterValue: '',
      isEnable: isEnableOptions[1],
    });

    /**
     ********************************************************************
     * 以下為人機互動區塊(on 開頭)
     * @function onActionCreate 按下新增的跳窗
     * @function onActionEdit 按下每筆 item 後的編輯按鈕跳窗
     * @function onActionDelete 按下每筆 item 後的刪除按鈕跳窗
     * @function onSubmitForm 當「新增」或「編輯」按下確認送出後的處理
     * @function onClearFormData 當「編輯」的跳窗關閉後清空 Form 資料四
     * @function onFilter 當按下「查詢」按鈕時的處理
     * @function onClearFilter 當按下「清除條件」按鈕時的處理
     * @function onPaginationRequest 當「分頁」或「列表排序」被點選時的處理
     ********************************************************************
     */

    const onActionCreate = () => {
      isCreate.value = true;
      isOpened.value = true;
    };

    const onActionEdit = async (rows) => {
      fetchItemForEdit.value = await fetchItem(rows.seqNo);
      isCreate.value = false;
      isOpened.value = true;
    };

    const onActionDelete = (rows) => {
      useConfirmDialog($q).onOk(() => {
        fetchDelete(rows.seqNo); // COPYDANIEL: primary key
      });
    };

    const onSubmitForm = (formData) => {
      if (isCreate.value) {
        fetchCreate(formData).then(() => {
          fetchItems();
        });
        return;
      }

      fetchUpdate(formData).then(() => {
        fetchItems();
      });
    };

    const onClearFormData = () => {
      fetchItemForEdit.value = {};
    };

    const onFilter = () => {
      pagination.value.sortBy = 'createDt';
      fetchItems(true);
    };

    const onClearFilter = () => {
      filter.filterItem = filterItemOptions[0];
      filter.filterValue = '';
      pagination.value.sortBy = defaultSortBy;
      fetchItems(true);
    };

    const onPaginationRequest = async (prop) => {
      pagination.value = prop.pagination;
      store.commit(`app/setRowsPerPage`, pagination.value.rowsPerPage);
      await fetchItems();
    };

    /**
     ********************************************************************
     * 以下為透過 Vuex store action(dispatch) 打 API 的區塊(fetch 開頭)
     * @function fetchCreate 新增表單送出後
     * @function fetchUpdate 編輯表單送出後
     * @function fetchDelete 刪除該 item 的資料(帶 ID)
     * @function fetchItem 抓取單筆資料(帶 ID)
     * @function fetchItems 抓取整列資料用
     ********************************************************************
     */

    const fetchCreate = (postData) => {
      return store.dispatch(`${moduleName}/fetchCreate`, postData).then(() => {
        useSuccessNotify($q, `新增成功!`);
        isOpened.value = false;
        onClearFormData();
      });
    };

    const fetchUpdate = (postData) => {
      return store.dispatch(`${moduleName}/fetchUpdate`, postData).then(() => {
        useSuccessNotify($q, `更新成功!`);
        isOpened.value = false;
        onClearFormData();
      });
    };

    const fetchDelete = (id) => {
      return store.dispatch(`${moduleName}/fetchDelete`, id).then(() => {
        useSuccessNotify($q, `刪除成功`);
        if (
          pagination.value.rowsNumber % pagination.value.rowsPerPage === 1 &&
          pagination.value.page > 1
        ) {
          pagination.value.page -= 1;
        }
        fetchItems(false);
      });
    };

    const fetchItem = (id) => {
      return store.dispatch(`${moduleName}/fetchItemById`, id);
    };

    const fetchItems = (reset) => {
      if (reset) {
        pagination.value.page = 1;
      }
      const queryFilter = {
        [filter.filterItem.value]: filter.filterValue || undefined,
        isEnable: filter.isEnable.value,
        page: pagination.value.page,
        rowsPerPage: pagination.value.rowsPerPage,
        sortColumn: pagination.value.sortBy || defaultSortBy,
        sortOrder: pagination.value.descending ? 'DESC' : 'ASC',
      };
      return store
        .dispatch(`${moduleName}/fetchItems`, queryFilter)
        .then((res) => {
          rows.value = store.getters[`${moduleName}/getItems`];
          pagination.value.rowsNumber = res.total;
          return res;
        })
        .catch(() => {
          rows.value = [];
        });
    };

    /**
     ********************************************************************
     * 以下區塊為 Vue Life Cycle 區塊
     ********************************************************************
     */

    onMounted(() => {
      fetchItems(true);
    });

    return {
      columns,
      rows,
      filterItemOptions,
      isEnableOptions,
      routeTitle,
      isOpened,
      isCreate,
      fetchItemForEdit,
      pagination,
      rowsPerPageOptions,
      filter,
      onActionCreate,
      onActionEdit,
      onActionDelete,
      onSubmitForm,
      onClearFormData,
      onPaginationRequest,
      onFilter,
      onClearFilter,
    };
  },
};
</script>

<style lang="scss" scoped>
.filter-isenable {
  min-width: 100px;
}
</style>
